using UnityEngine;
using System.Collections;

public class BasicAttack : MonoBehaviour {

    protected Unit u;
    private RaycastHit hit;
    GameObject clone;

    private Vector3 halfVec;

    // Use this for initialization
    void Start () {
        Screen.lockCursor = true;
        u = GetComponent<Unit>();
        halfVec = new Vector3(Screen.width/2, Screen.height/2, 0);
    }
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(halfVec), out hit, 100.0f))
            {
                clone = Instantiate(u.projectile, u.projectileSpawnPoint.position, Quaternion.identity) as GameObject;
                clone.transform.LookAt(hit.point);
            }
            else
            {
                clone = Instantiate(u.projectile, u.projectileSpawnPoint.position, Quaternion.identity) as GameObject;
                clone.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);
            }
           


        }
       
	}

}
