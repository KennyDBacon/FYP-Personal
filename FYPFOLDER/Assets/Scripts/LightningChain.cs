using UnityEngine;
using System.Collections;

public class LightningChain : MonoBehaviour {

    public ArrayList targets;
    public GameObject currentTarget;
    public int damage;

    private int hitCount = 5;

    private bool newChain;
    //private SphereCollider collider;
    private float maxColliderSize = 4.0f;

    private Collider[] col;

    void Start()
    {
        targets = new ArrayList();

        newChain = true;
        //collider = this.GetComponent<SphereCollider>();
	}
	
	void Update () {
        if (newChain)
        {
            targets.Add(currentTarget);
            col = Physics.OverlapSphere(currentTarget.transform.position, 4.0f);

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

            //collider.radius = 0.2f;
            newChain = false;

            int i = 0;
            if (i < col.Length)
            {
                Debug.Log("Hit");

                if (!col[i].isTrigger)
                {
                    Debug.Log("Hi2");
                    Debug.Log(col[i].transform.root);
                    Debug.Log(col[i].transform.root.tag);
                    Debug.Log(col[i].transform.root.GetComponent<Unit>());
                    if (col[i].transform.root.GetComponent<Unit>() != null)
                    {
                        Debug.Log("Hi3");
                        if (!col[i].transform.root.GetComponent<Unit>().isAllyTeam)
                        {
                            Debug.Log("Hi4");
                            HitEnemy(col[i]);
                        }
                    }
                }

                i++;
            }
        }

        //if (collider.radius < maxColliderSize)
        //{
        //    collider.radius += Time.deltaTime;
        //    this.transform.position = currentTarget.transform.position;
        //}
        //if 
        //{
        //    Destroy(this.gameObject);
        //}
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("!23");
        /*Debug.Log(col.isTrigger);
        Debug.Log(col.transform.root.tag + "     " + col.transform);
        Debug.Log(!col.transform.root.gameObject.Equals(currentTarget));*/
        //Debug.Log(col.transform.root.gameObject + "    " + currentTarget);
        //if (!col.isTrigger && col.transform.root.tag == "Enemy" && !col.transform.root.gameObject.Equals(currentTarget))
        //{
        //    Debug.Log("Hi2");
        //    if (col.transform.root.GetComponent<Unit>() != null)
        //    {
        //        Debug.Log("Hi3");
        //        if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
        //        {
        //            Debug.Log("Hi4");
        //            HitEnemy(col);
        //        }
        //    }
        //}
    }

    void OnTriggerStay(Collider col)
    {
        /*if (col.transform.root.tag == "Enemy" && col.transform.root.gameObject != currentTarget)
        {
            if (col.transform.root.GetComponent<Unit>() != null)
            {
                if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
                {
                    HitEnemy(col);
                }
            }
        }*/
    }

    void HitEnemy(Collider col)
    {
        bool isTagged = false;

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

        foreach (Collider c in col)
        {
            Gizmos.DrawWireSphere(c.transform.position, 4.0f);
        }
    }
}
