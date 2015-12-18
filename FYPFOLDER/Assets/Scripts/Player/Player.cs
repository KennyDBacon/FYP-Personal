using UnityEngine;
using System.Collections;

public class Player : Unit {


    
   public GameObject[] Proj = new GameObject[3];
    private RaycastHit hit;
    GameObject clone;
    private Vector3 halfVec;
    public int speed;
    public float timer;
    private float coolDown = 0.2f, attackInt = 0;

	private float fireCd = 5.0f, lightningCd = 5.0f, earthCd = 3.0f;
	private float fireInt = 5, lightningInt = 5, earthInt = 3;
    // Use this for initialization
    void Start () {
        halfVec = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        damage = 10;
        speed = 20;
        timer = 0f;
        attackInt = 0;
    }
	
	// Update is called once per frame
	void Update () {
        attackInt += Time.deltaTime;
		fireInt += Time.deltaTime;
		lightningInt += Time.deltaTime;
		earthInt += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
		{        
			if (attackInt > coolDown)
			{
				attackInt = 0;
				BasicRayCast();
			}
			else
				return;
         
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //FIRE
			if (fireInt > fireCd)
			{
				fireInt = 5;
				FireRayCast();
			}
			else
				return;
        }
        //TO DO : ICE
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Lightning
			if(lightningInt > lightningCd)
			{
				lightningInt = 0;
				LightRayCast();
			}
			else
				return;
            

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //EARTH
			if(earthInt > earthCd)
			{
				earthInt = 0;
            	EarthRayCast();
			}
			else
				return;
        }

	}
    void BasicRayCast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(halfVec), out hit, 100.0f))
        {
            clone = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(hit.point);
        }
        else
        {
            clone = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);
        }
    }
    void FireRayCast()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(halfVec), out hit, 100.0f))
        {
            clone = Instantiate(Proj[0], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(hit.point);
        }
        else
        {
            clone = Instantiate(Proj[0], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);
        }
    }
    void EarthRayCast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(halfVec), out hit, 100.0f))
        {
            clone = Instantiate(Proj[2], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(hit.point);
        }
        else
        {
            clone = Instantiate(Proj[2], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);
        }
    }
    void LightRayCast()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(halfVec), out hit, 100.0f))
        {
            clone = Instantiate(Proj[3], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(hit.point);
            //   float dist = hit.distance;
            // Debug.Log(dist + "/" + hit.collider.gameObject.name );
        }
        else
        {
            clone = Instantiate(Proj[3], projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            clone.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);
        }
    }

}
