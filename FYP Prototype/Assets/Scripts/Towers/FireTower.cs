using UnityEngine;
using System.Collections;

public class FireTower : Tower {

    public GameObject fireParticle;

    void Start()
    {
        isRotating = true;
    }

	void Update () {
	    if(target != null)
        {
            TowerAction();

            fireParticle.GetComponent<ParticleSystem>().Play();

            if(attackTimer >= unit.attackInterval)
            {
                attackTimer = 0;
                foreach(Unit enemy in fireParticle.GetComponent<Flamethrower>().GetEnemyList)
                {
                    enemy.health -= unit.damage;
                }
            }
        }
        else
        {
            fireParticle.GetComponent<ParticleSystem>().Stop();
        }
	}
}
