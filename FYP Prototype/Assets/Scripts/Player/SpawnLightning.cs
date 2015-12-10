using UnityEngine;
using System.Collections;

public class SpawnLightning : MonoBehaviour
{

    private GameObject player;
    private int speed, damage;
    private double timer;
    public GameObject lightshotPrefab;
    // public Unit target;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = player.GetComponent<Player>().timer;

    }

    // Update is called once per frame

    void Update()
    {

        speed = player.GetComponent<Player>().speed + 20;
        damage = player.GetComponent<Player>().damage;


        // Debug.Log(speed + "/D" + damage + "/t"  + timer);
        transform.position += transform.forward * Time.deltaTime * speed;
        timer += 1.0F * Time.deltaTime;
        if (timer >= 2)
        {

            Destroy(gameObject);
        }



    }
	void OnTriggerEnter(Collider col)
	{
		Vector3 getVect = new Vector3(col.transform.position.x, col.transform.position.y + 2.0f, col.transform.position.z);
		//Debug.Log(getVect);
		GameObject clone = Instantiate(lightshotPrefab, getVect, Quaternion.identity) as GameObject;
		Destroy(gameObject);
	}
 /*   void OnTriggerEnter(Collider col)
    {
        Debug.Log(col + "////" + col.transform.root);
        //   Debug.Log(col.transform.root.GetComponent<Unit>());
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            Unit temp = col.transform.root.GetComponent<Unit>();
            if (!temp.isAllyTeam && temp != null)
            {
                Debug.Log(col.transform.position.y);
                Vector3 getVect = new Vector3(col.transform.position.x, col.transform.position.y + 2.0f, col.transform.position.z);
                Debug.Log(getVect);
                GameObject clone = Instantiate(lightshotPrefab, getVect, Quaternion.identity) as GameObject;
    
                // temp.health -= damage;
                Destroy(gameObject);
                //Debug.Log("health: " + temp.health + "; damage: " + damage);
            }
        }
        Destroy(gameObject);
    }*/
}