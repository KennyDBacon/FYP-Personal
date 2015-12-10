using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuilder : MonoBehaviour
{
    public List<GameObject> towers;
    public List<Tower> recentlyBuiltTowers;
    public int towerIndex;

    public bool isBuildMode = false;

    [SerializeField]
    private Camera upperCam;
    [SerializeField]
    private GameObject towerMenu;
    private GameObject selectedTower;

    private RaycastHit hit;
    private int layerMask;
    private GameObject ghostTower;
    private int unitCost;
    private Vector3 moveToPos;
	
    void Awake ()
    {
        layerMask = (1 << 8);
    }     

    void Update ()
    {
        if (isBuildMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndBuildMode();
            }

            ghostTower.GetComponent<BuildCheck>().Check();

            if (Input.GetMouseButtonDown(0) && !GameManager.manager.eventSystem.IsPointerOverGameObject())
            {
                if (ghostTower.GetComponent<BuildCheck>().isBuildable)
                {
                    Instantiate(towers[towerIndex], ghostTower.transform.position, Quaternion.identity);
                    GameManager.manager.AddShards(-unitCost);
                    if (GameManager.manager.shard < unitCost)
                    {
                        EndBuildMode();
                    }
                }
            }

            if (Physics.Raycast(upperCam.ScreenPointToRay(Input.mousePosition), out hit, 100.0f, layerMask))
            {
                moveToPos.x = Mathf.Round(hit.point.x);
                moveToPos.z = Mathf.Round(hit.point.z);
                moveToPos.y = GameManager.manager.terrain.SampleHeight(moveToPos);

                ghostTower.transform.position = moveToPos;
            }
        }	
        else if(GameManager.manager.isBuildPhase)
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.manager.eventSystem.IsPointerOverGameObject())
            {
                if(Physics.Raycast(upperCam.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
                {
                    if(hit.transform.root.CompareTag("Building") && hit.transform.root.GetComponent<BasicTower>() != null)
                    {
                        towerMenu.SetActive(true);
                        for (int i = 0; i < 4; ++i)
                        {
                            towerMenu.transform.GetChild(i).gameObject.SetActive(true);
                        }
                        selectedTower = hit.transform.gameObject;
                    }
                    else if (hit.transform.root.CompareTag("Building") && hit.transform.root.GetComponent <Unit>().isAllyTeam)
                    {
                        towerMenu.SetActive(true);
                        for (int i = 0; i < 4; ++i)
                        {
                            towerMenu.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        selectedTower = hit.transform.gameObject;
                    }
                    else
                    {
                        towerMenu.SetActive(false);
                    }
                }
            }
        }
        if (towerMenu.activeSelf)
        {
            towerMenu.transform.position = upperCam.WorldToScreenPoint(hit.transform.position);
        }
    }

    public void StartBuildMode (bool isWall)
    {
        if(GameManager.manager.isBuildPhase)
        {
            if (isWall)
            {
                towerIndex = 0;
            }
            else
            {
                towerIndex = 1;
            }
            if (GameManager.manager.shard > towers[towerIndex].GetComponent<Unit>().cost)
            {
                isBuildMode = true;
                GameManager.manager.strategicViewUI.SetActive(false);
                GameManager.manager.buildModeText.SetActive(true);
                ghostTower = Instantiate(towers[towerIndex]);
                ghostTower.AddComponent<BuildCheck>();
                if (towerIndex != 0)
                {
                    ghostTower.GetComponent<SphereCollider>().enabled = false;
                }
                ghostTower.GetComponent<NavMeshObstacle>().carveOnlyStationary = false;
                unitCost = ghostTower.GetComponent<Unit>().cost;
            }
        }
    }

    public void EndBuildMode ()
    {
        if (isBuildMode)
        {
            isBuildMode = false;
            ghostTower.SetActive(false);
            Destroy(ghostTower);
            GameManager.manager.strategicViewUI.SetActive(true);
            GameManager.manager.buildModeText.SetActive(false);
        }
    }

    public void UpgradeTower (int index)
    {
        if (GameManager.manager.shard > towers[index].GetComponent<Unit>().cost)
        {
            towerMenu.SetActive(false);
            Instantiate(towers[index], selectedTower.transform.position, Quaternion.identity);
            GameManager.manager.AddShards(-towers[index].GetComponent<Unit>().cost);
            selectedTower.SetActive(false);
            Destroy(selectedTower);
        }
    }

    public void SellTower ()
    {
        towerMenu.SetActive(false);
        GameManager.manager.AddShards(35);
        selectedTower.SetActive(false);
        Destroy(selectedTower);
    }
}
