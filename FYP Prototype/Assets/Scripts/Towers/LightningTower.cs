using UnityEngine;
using System.Collections;

public class LightningTower : Tower {

    public GameObject lightningPrefab;

    public float attackTimer;

    void Start()
    {
        isUpgradable = true;
    }

    void Update () {
        if (target != null)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= 5.0f)
            {
                attackTimer = 0.0f;
                if (target.GetComponent<LightningHit>() == null)
                {
                    GameObject go = Instantiate(lightningPrefab, transform.position, Quaternion.identity) as GameObject;
                    go.GetComponent<LightningChain>().Damage = unit.damage;

                    target.gameObject.AddComponent<LightningHit>().lightningChain = go.GetComponent<LightningChain>();
                }
            }

            //unit.Attack(target);
        }
	}
}
