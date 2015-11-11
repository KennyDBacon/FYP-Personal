using UnityEngine;
using System.Collections;

public class LightningHit : MonoBehaviour {

    public LightningChain lightningChain;
    private SphereCollider collider;

    public float lifetime;

	// Use this for initialization
	void Start () {
        lightningChain.ChainedEnemy.Add(this.gameObject.GetComponent<Enemy>());

        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (collider != null)
        {
            lifetime += Time.deltaTime;
            if (lifetime < lightningChain.ChainTimer)
            {
                collider.radius += 0.08f;
            }
            else
            {
                Destroy(collider);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        TagTarget(col);
    }

    void OnTriggerStay(Collider col)
    {
        TagTarget(col);
    }

    void TagTarget(Collider col)
    {
        if (lightningChain.TargetAmount < lightningChain.MaxTarget)
        {
            if (col.gameObject.GetComponent<Enemy>() != null)
            {
                bool isRestruck = lightningChain.checkRestruck(col);

                if (!isRestruck)
                {
                    lightningChain.action(col);
                }
            }
        }
    }

    public void removeAttachment()
    {
        Destroy(collider);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (collider != null)
            Gizmos.DrawWireSphere(transform.position, collider.radius);
    }
}
