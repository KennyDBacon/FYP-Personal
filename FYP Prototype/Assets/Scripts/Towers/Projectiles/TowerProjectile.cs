using UnityEngine;
using System.Collections;

public class TowerProjectile : MonoBehaviour
{
    public Unit target;
    public int damage;
    public float speed = 10.0f;

	void Update ()
    {
        if (target != null)
        {
            gameObject.transform.LookAt(target.gameObject.transform);
            transform.position += speed * transform.forward * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter (Collider col)
    {
        if (!col.isTrigger && col.GetComponent<Unit>() != null && !col.GetComponent<Unit>().isAllyTeam && target != null)
        {
            target.health -= damage;
            if (target.health <= 0)
            {
                Destroy(target.gameObject);
                target = null;
            }
            Destroy(gameObject);
        }
    }
}
