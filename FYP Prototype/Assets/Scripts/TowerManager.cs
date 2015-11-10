using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {

    public CameraControl playerCam;
    public Camera upperCamera;
    public List<GameObject> towers;

    private bool isBuildMode = false;
    private GameObject ghostTower;

	void Start () {
        //playerCam = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControl>();
        //upperCamera = GameObject.FindGameObjectWithTag("SubCamera").GetComponent<Camera>();
	}
	
	void Update () {
	    if(playerCam.UpperCam.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                isBuildMode = !isBuildMode;

                if (isBuildMode)
                {
                    ghostTower = Instantiate(towers[0], Vector3.zero, Quaternion.identity) as GameObject;
                }
                else
                {
                    Destroy(ghostTower);
                }
            }

            if(isBuildMode)
            {
                Ray ray = upperCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    ghostTower.transform.position = new Vector3(Mathf.Round(hit.point.x),
                                                                ghostTower.transform.localScale.y / 2,
                                                                Mathf.Round(hit.point.z));
                }

                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (ghostTower.GetComponent<TowerBuilding>().IsBuildable)
                    {
                        GameObject newTower = Instantiate(towers[0], ghostTower.transform.position, Quaternion.identity) as GameObject;
                        newTower.GetComponent<BoxCollider>().isTrigger = false;
                        newTower.GetComponent<NavMeshObstacle>().enabled = true;
                        newTower.GetComponent<SphereCollider>().enabled = true;
                        newTower.GetComponent<Unit>().enabled = true;
                        newTower.GetComponent<BasicTower>().enabled = true;
                        newTower.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                        Destroy(newTower.GetComponent<TowerBuilding>());
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
}
