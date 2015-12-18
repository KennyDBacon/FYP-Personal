using UnityEngine;
using System.Collections;

public class basicPProjectile : MonoBehaviour {

    private GameObject player;
    private int speed;
    private int damage;
    private double timer;
   // public Unit target;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        timer = player.GetComponent<Player>().timer;
        speed = player.GetComponent<Player>().speed;
        damage = player.GetComponent<Player>().damage;

    }
	
	// Update is called once per frame

	void Update () {

     

       // Debug.Log(speed + "/D" + damage + "/t"  + timer);
        transform.position += transform.forward * Time.deltaTime * speed;
        timer += 1.0F * Time.deltaTime;
        if (timer >= 2)
        {
          
            Destroy(gameObject);
        }



    }
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col + "////" + col.transform.root);
     //   Debug.Log(col.transform.root.GetComponent<Unit>());
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            Unit temp = col.transform.root.GetComponent<Unit>();
            if (!temp.isAllyTeam && temp != null)
            {
               
                temp.health -= damage;
                Destroy(gameObject);
              //Debug.Log("health: " + temp.health + "; damage: " + damage);
            
                if (temp.health <= 0)
                {
                 // Debug.Log("DED");
                    Destroy(temp.gameObject);
                    temp = null;
                    Destroy(gameObject);
                }
               
            }
        }

    }
    }
