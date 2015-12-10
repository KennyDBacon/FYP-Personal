using UnityEngine;
using System.Collections;

public class lightning : MonoBehaviour
{

    private GameObject player;
    private int speed, damage;
    private double timer;
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

        damage = player.GetComponent<Player>().damage + 20;
        timer += 1.0F * Time.deltaTime;
        if (timer >= 1)
        {
            Destroy(gameObject);
        }



    }
    void OnTriggerEnter(Collider col)
    {
       // Debug.Log(col + "////" + col.transform.root);
        //   Debug.Log(col.transform.root.GetComponent<Unit>());
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            Unit temp = col.transform.root.GetComponent<Unit>();
            if (!temp.isAllyTeam && temp != null)
            {
              
                temp.health -= damage;
                
              //  Debug.Log("health: " + temp.health + "; damage: " + damage);

                if (temp.health <= 0)
                {
                    // Debug.Log("DED");
                    Destroy(temp.gameObject);
                    temp = null;
                    Destroy(gameObject);
                }

            }
        }
      //  Destroy(gameObject);
    }
}