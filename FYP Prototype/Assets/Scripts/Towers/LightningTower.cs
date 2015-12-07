using UnityEngine;
using System.Collections;

public class LightningTower : Tower {

    public GameObject lightningChain;

    void Update () {
        if (target != null)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= 5.0f)
            {
                attackTimer = 0.0f;
                if (target.GetComponent<LightningHit>() == null && target.tag == "Enemy")
                {
                    GameObject chain = Instantiate(lightningChain, target.transform.position, Quaternion.identity) as GameObject;
                    chain.GetComponent<LightningChain>().targetGO = target.gameObject;
                    chain.GetComponent<LightningChain>().damage = unit.damage;

                    target.gameObject.AddComponent<LightningHit>();
                }
            }
        }
	}
}
