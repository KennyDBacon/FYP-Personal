using UnityEngine;
using System.Collections;

public class fireProjectile : MonoBehaviour {

    private GameObject player;
    private int speed, damage;
    private double timer;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        timer = player.GetComponent<Player>().timer;
    }
	
	// Update is called once per frame
	void Update () {
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
       // Debug.Log(col + "/" + col.transform.root);
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            Unit temp = col.transform.root.GetComponent<Unit>();
          
            if (!temp.isAllyTeam && temp != null)
            {
              Debug.Log("health: " + temp.health + "; damage: " + damage);
                temp.health -= damage;
                //create explosion
                Collider[] Arround = Physics.OverlapSphere(transform.position, 2);
                foreach (Collider inExp in Arround)
                {
                    if (inExp.transform.tag.Equals("Enemy"))
                    {
                         Unit t = inExp.transform.root.GetComponent<Unit>();
                        t.health -= damage +100;
                         Debug.Log("t" + t + "th" + t.health);
                        if (t.health <= 0)
                        {
                          //  Debug.Log("DED2");
                            Destroy(t.gameObject);
                            t = null;
                        }
                    }
                        


                }

                if (temp.health <= 0)
                {
                   // Debug.Log("DED");
                    Destroy(temp.gameObject);
                    temp = null;
                }

                Destroy(gameObject);
            }
        }

    }
}
