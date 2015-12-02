using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
    public GameObject level;
    public CameraControl playerCam;
    public Camera upperCamera;
    public List<GameObject> towers;

    private bool isBuildMode = false;
    private GameObject ghostTower;
    private Vector3 ghostTowerPos;

    // For testing purpose only
    private int towerIndex = 0;
    private bool isNext = false;

    private bool onUpMenu = false;
    private Vector3 uiPos;
    private Transform selectedTower;

	void Start () {
        //playerCam = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControl>();
        //upperCamera = GameObject.FindGameObjectWithTag("SubCamera").GetComponent<Camera>();
	}
	
	void Update () {
	    if(playerCam.UpperCam.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = upperCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tower")
                    {
                        if (hit.collider.GetComponent<BoxCollider>() != null)
                        {
                            onUpMenu = true;
                            selectedTower = hit.transform;
                        }
                    }
                }
            }

            if (onUpMenu)
            {
                uiPos = upperCamera.WorldToScreenPoint(selectedTower.position);
                Debug.Log(uiPos);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                isBuildMode = !isBuildMode;

                if (isBuildMode)
                {
                    ghostTower = Instantiate(towers[towerIndex], Vector3.zero, Quaternion.identity) as GameObject;
                }
                else
                {
                    Destroy(ghostTower);
                }
            }

            if (isBuildMode)
            {
                // Testing purpose only //
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    towerIndex++;
                    isNext = true;
                }
                else if (Input.GetKeyDown(KeyCode.F2))
                {
                    towerIndex--;
                    isNext = true;
                }

                if (towerIndex < 0 || towerIndex >= towers.Count)
                {
                    towerIndex = 0;
                }

                if(isNext)
                {
                    isNext = false;
                    Destroy(ghostTower.gameObject);
                    ghostTower = Instantiate(towers[towerIndex], Vector3.zero, Quaternion.identity) as GameObject;
                }
                ///////////////////////////

                Ray ray = upperCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    ghostTowerPos.x = Mathf.Round(hit.point.x);
                    ghostTowerPos.y = ghostTower.transform.localScale.y / 2;
                    ghostTowerPos.z = Mathf.Round(hit.point.z);
                    
                    ghostTower.transform.position = ghostTowerPos;
                }

                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (ghostTower.GetComponent<TowerBuilding>().IsBuildable)
                    {
                        GameObject newTower = Instantiate(towers[towerIndex], ghostTower.transform.position, Quaternion.identity) as GameObject;
                        newTower.GetComponent<BoxCollider>().isTrigger = false;
                        newTower.GetComponent<NavMeshObstacle>().enabled = true;
                        newTower.GetComponent<Unit>().enabled = true;
                        newTower.GetComponent<BoxCollider>().size =Vector3.one;
                        Destroy(newTower.GetComponent<TowerBuilding>());

                        if (newTower.GetComponent<SphereCollider>() != null)
                            newTower.GetComponent<SphereCollider>().enabled = true;

                        switch(towerIndex)
                        {
                            case 0: newTower.GetComponent<BasicTower>().enabled = true;
                                break;
                            case 1: newTower.GetComponent<FireTower>().enabled = true;
                                break;
                            case 2: newTower.GetComponent<IceTower>().enabled = true;
                                break;
                            case 3: newTower.GetComponent<EarthTower>().enabled = true;
                                break;
                            //case 4: newTower.GetComponent <LightningTower>().enabled = true;
                                //break;
                        }
                    }
                }
            }
        }
        else
        {
            isBuildMode = false;
            if(ghostTower != null)
            {
                Destroy(ghostTower);
            }
        }
	}

    void OnGUI()
    {
        if (onUpMenu)
        {
            GUI.Button(new Rect(uiPos.x, uiPos.y, 100, 40), "Test");
        }
    }
}
