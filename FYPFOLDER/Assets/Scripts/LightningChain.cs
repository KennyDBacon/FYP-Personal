using UnityEngine;
using System.Collections;

public class LightningChain : MonoBehaviour {

    public ArrayList targets;
    public GameObject currentTarget;
    public int damage;

    private int hitCount = 5;

    private bool newChain;
    private SphereCollider collider;
    private float maxColliderSize = 4.0f;

    void Start()
    {
        targets = new ArrayList();

        newChain = true;
        collider = this.GetComponent<SphereCollider>();
	}
	
	void Update () {
        if (newChain)
        {
            targets.Add(currentTarget);

            // Don't damage the first one again
            if (targets.Count > 1)
            {
                currentTarget.transform.root.GetComponent<Unit>().health -= damage;

                if (currentTarget.transform.root.GetComponent<Unit>().health <= 0)
                {
                    Destroy(currentTarget);
                    Destroy(this.gameObject);
                }
            }

            hitCount--;

            if (hitCount <= 0)
            {
                if (this.gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            }

            collider.radius = 0.2f;
            newChain = false;
        }

        if (collider.radius < maxColliderSize)
        {
            collider.radius += Time.deltaTime;
            this.transform.position = currentTarget.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger)
        {
            if (col.transform.root.tag == "Enemy" && col.transform.root.gameObject != currentTarget)
            {
                Debug.Log("Enemy");
                if (col.transform.root.GetComponent<Unit>() != null)
                {
                    if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
                    {
                        HitEnemy(col);
                    }
                }
            }
        }
    }

    void ontriggerstay(Collider col)
    {
        if (!col.isTrigger)
        {
            if (col.transform.root.tag == "enemy" && col.transform.root.gameObject != currentTarget)
            {
                if (col.transform.root.GetComponent<Unit>() != null)
                {
                    if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
                    {
                        HitEnemy(col);
                    }
                }
            }
        }
    }

    void HitEnemy(Collider col)
    {
        bool isTagged = false;
        Debug.Log("Check");
        foreach (GameObject taggedTarget in targets)
        {
            if (col.transform.root.gameObject == taggedTarget)
            {
                isTagged = true;
                break;
            }
        }

        if (!isTagged)
        {
            newChain = true;

            currentTarget = col.transform.root.gameObject;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(currentTarget.transform.position, collider.radius);
    }
}
