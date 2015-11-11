using UnityEngine;
using System.Collections;

public class BasicTower : Tower {
    
    void Update()
    {
        if (target != null)
        {
            TowerAction();

            // Temporary to make the tower spawn projectile
            if (attackTimer >= unit.attackInterval)
            {
                attackTimer = 0;

                GameObject towerProjectile = Instantiate(unit.projectile, unit.projectileSpawnPoint.position, Quaternion.identity) as GameObject;
                towerProjectile.GetComponent<TowerProjectile>().target = target;
                towerProjectile.GetComponent<TowerProjectile>().damage = unit.damage;
            }
        }
    }
}
