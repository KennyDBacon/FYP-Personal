using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    public bool isAllyTeam;
    public int health;
    public int damage;
    public Unit target = null;

    //Alpha (Removing later. Moving to towerclass)
    public float attackInterval;
    protected float attackTimer = 0.0f;

    //Ranged units
    public GameObject projectile = null;
    public Transform projectileSpawnPoint;

    void Start ()
    {
        attackTimer = attackInterval;
    }

    protected virtual void Update ()
    {
        attackTimer += Time.deltaTime;
    }

    protected void Attack()
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

    protected virtual void MeleeAttack()
    {
        Damage();
    }

    private void RangedAttack()
    {
        if (projectile != null)
        {
            GameObject towerProjectile = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
            towerProjectile.GetComponent<Unit>().target = target;
            towerProjectile.GetComponent<Unit>().damage = damage;
        }
    }

    public virtual void AliveCheck()
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

    protected virtual void Damage()
    {
        target.health -= damage;
        target.AliveCheck();
    }
}
