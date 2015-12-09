using UnityEngine;
using System.Collections;

public class LightningTower : Tower {

    public GameObject lightningChain;

    private ArrayList targets;

    void Update () {
        if (target != null)
        {
            TowerAction();

            if (attackTimer >= unit.attackInterval)
            {
                attackTimer = 0;

                // Damage the target and check if alive
                target.health -= unit.damage;
                CheckTargetAlive();

                if (target != null)
                {
                    GameObject chain = Instantiate(lightningChain, target.transform.position, Quaternion.identity) as GameObject;

                    // Set first target
                    chain.GetComponent<LightningChain>().currentTarget = target.gameObject;

                    // Set the damage of the lightning chain
                    chain.GetComponent<LightningChain>().damage = unit.damage;
                }
            }
        }
	}
}
