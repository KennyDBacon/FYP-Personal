using UnityEngine;
using System.Collections;

public class LightningChain : MonoBehaviour {

    public GameObject targetGO;
    public int damage;

    public int hitCount = 5;

    private bool newChain = true;
    private SphereCollider collider;

	void Start () {
        collider = this.GetComponent<SphereCollider>();
	}
	
	void Update () {
        if (newChain)
        {
            targetGO.transform.root.GetComponent<Unit>().health -= damage;

            if (targetGO.transform.root.GetComponent<Unit>().health <= 0)
            {
                Destroy(targetGO);
                Destroy(this.gameObject);
            }

            Debug.Log(targetGO.transform.position);
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

        collider.radius += Time.deltaTime;
        this.transform.position = targetGO.transform.position;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
            {
                if (col.gameObject.GetComponent<LightningHit>() == null && col.tag == "Enemy")
                {
                    HitEnemy(col);
                }
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.root.GetComponent<Unit>() != null)
        {
            if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
            {
                if (col.gameObject.GetComponent<LightningHit>() == null && col.tag == "Enemy")
                {
                    HitEnemy(col);
                }
            }
        }
    }

    void HitEnemy(Collider col)
    {
        newChain = true;

        targetGO = col.gameObject;

        col.gameObject.AddComponent<LightningHit>();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.gameObject.transform.position, collider.radius);
    }
}
