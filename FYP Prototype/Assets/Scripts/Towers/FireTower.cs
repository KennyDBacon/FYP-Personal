using UnityEngine;
using System.Collections;

public class FireTower : Tower {

    public GameObject fireParticle;

    public float attackTimer;

    void Start()
    {
        isRotating = true;
    }

	void Update () {
	    if(target != null)
        {
            TowerAction();

            fireParticle.GetComponent<ParticleSystem>().Play();

            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                attackTimer = 0.6f;
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
