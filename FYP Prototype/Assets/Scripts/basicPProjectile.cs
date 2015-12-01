using UnityEngine;
using System.Collections;

public class basicPProjectile : MonoBehaviour {

    public int damage;
    public float speed = 10.0f;
    private float timer;
   // public Unit target;
    // Use this for initialization
    void Start () {
            
        damage = 10;
	}
	
	// Update is called once per frame
	void Update () {
        
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
        if(col.GetComponent<Unit>() != null)
        {
            Unit temp = col.GetComponent<Unit>();
            if (!temp.isAllyTeam && temp != null)
            {
                temp.health -= damage;
               // Debug.Log("health: " + temp.health + "; damage: " + damage);
              //  Debug.Log("HITLAH");  
                if (temp.health <= 0)
                {
                  //  Debug.Log("DED");
                    Destroy(temp.gameObject);
                    temp = null;
                }
                Destroy(gameObject);
            }
        }

    }
    }
