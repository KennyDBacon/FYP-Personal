using UnityEngine;
using System.Collections;

public class EarthProjectile : MonoBehaviour
{

    private GameObject player;
    private int speed, damage;
    private double timer;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = player.GetComponent<Player>().timer;
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<Player>().speed;
        damage = player.GetComponent<Player>().damage;

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
      //  Debug.Log(col + "////" + col.transform.root);
       // Debug.Log(col.transform.root.GetComponent<Unit>());
        if (col.transform.GetComponent<Unit>() != null)
        {
             Debug.Log("TESTER");
            Unit temp = col.transform.GetComponent<Unit>();
            Debug.Log(temp);
            if (temp.isAllyTeam && temp != null)
            { 
                Debug.Log("DSA");
                temp.health += damage;
                Debug.Log("health: " + temp.health + "; damage: " + damage);
                Destroy(gameObject);
              

                if (temp.health >= 500 )
                {
                     Debug.Log("in full Hp");
                        
                }

            }
        }

    }
}
