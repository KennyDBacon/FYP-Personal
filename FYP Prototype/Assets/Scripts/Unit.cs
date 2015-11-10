using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    public bool isAllyTeam;
    public int health;
    public int damage;
    public float attackInterval;
    public Unit target = null;

    //Alpha (Removing later)
    private float attackTimer = 0.0f;

    //Ranged units
    public GameObject projectile = null;
    public Transform projectileSpawnPoint;

    void Start ()
    {
        attackTimer = attackInterval;
    }

    void Update ()
    {
        attackTimer += Time.deltaTime;
    }

    public void Attack ()
    {
        if (attackTimer >= attackInterval)
        {
            if (projectile == null)
            {
                MeleeAttack();
            }
            else
            {
                RangedAttack();
            }
            attackTimer = 0;
        }
    }

    private void MeleeAttack()
    {
        target.health -= damage;
        target.AliveCheck();
    }

    private void RangedAttack()
    {
        if (projectile != null)
        {
            GameObject towerProjectile = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            towerProjectile.GetComponent<TowerProjectile>().target = target;
            towerProjectile.GetComponent<TowerProjectile>().damage = damage;
        }
    }

    public void AliveCheck()
    {
        if (health <= 0)
        {
            if (tag == "Player")
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
