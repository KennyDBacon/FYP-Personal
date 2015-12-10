using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    public bool isAllyTeam;
    public int maxHealth;
    public int health;
    public int damage;
    public Unit target = null;
    public int cost = 0;

    public float attackInterval;
    protected float attackTimer = 0.0f;

    //Ranged units
    public GameObject projectile = null;
    public Transform projectileSpawnPoint;

    void Awake ()
    {
        //REMOVE PLEASE KEVIN
        AliveCheck();
        /////////////
    }

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

        //PLEASE REMOVE THIS KEVIN
        if(CompareTag("Player"))
        {
            GameManager.manager.playerHealthText.text = (GameManager.manager.player.health + "/" + GameManager.manager.player.maxHealth);
            GameManager.manager.playerHealthBar.fillAmount = (float)GameManager.manager.player.health / (float)GameManager.manager.player.maxHealth;
        }
        else if (CompareTag("EndPoint"))
        {
            GameManager.manager.castleHealthText.text = (GameManager.manager.endPoint.health + "/" + GameManager.manager.endPoint.maxHealth);
            GameManager.manager.castleHealthBar.fillAmount = (float)GameManager.manager.endPoint.health / (float)GameManager.manager.endPoint.maxHealth;
        }
        //////////////////////

        if (health <= 0)
        {
            if (CompareTag("Player"))
            {
                gameObject.SetActive(false);
                GameManager.manager.SwitchToStrategic(false);
            }
            else if (CompareTag("EndPoint"))
            {
                GameManager.manager.LoseGame();
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
