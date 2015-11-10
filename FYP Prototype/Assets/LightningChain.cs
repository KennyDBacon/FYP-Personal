using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningChain : MonoBehaviour {
    
    private List<Enemy> chainedEnemy;
    private int damage;

    private int targetAmount;
    private int maxTarget = 5;

    private float chainTimer = 0.8f;
    private float totalLifetime;

    void Start () {
        chainedEnemy = new List<Enemy>();

        totalLifetime = maxTarget * chainTimer;
	}

    void Update()
    {
        totalLifetime -= Time.deltaTime;

        checkEndChain();

        foreach(Enemy enemy in chainedEnemy)
        {
            Debug.DrawLine(transform.position, enemy.transform.position);
        }
    }

    public bool checkRestruck(Collider col)
    {
        bool test = false;
        foreach(Enemy enemy in chainedEnemy)
        {
            if(col.gameObject.GetComponent<Enemy>() == enemy)
            {
                test = true;
                return true;
            }

            Debug.Log("Tagged: " + test);
            test = false;
        }
        
        return false;
    }

    public void action(Collider col)
    {
        targetAmount++;

        //col.gameObject.GetComponent<Unit>().health -= damage;
        if (col.gameObject.GetComponent<Unit>().health > 0)
        {
            //ChainedEnemy.Add(col.gameObject.GetComponent<Enemy>());
            col.gameObject.AddComponent<LightningHit>().lightningChain = this;
        }
        else
        {
            Destroy(col.gameObject);
        }
    }

    public void checkEndChain()
    {
        if(totalLifetime < 0 || targetAmount >= maxTarget)
        {
            Debug.Log(chainedEnemy.Count);
            foreach(Enemy enemy in ChainedEnemy)
            {
                if(enemy != null)
                {
                    // Remove the sphere collider then the script
                    if (enemy.GetComponent<LightningHit>() != null)
                    {
                        //enemy.GetComponent<LightningHit>().removeAttachment();
                        Destroy(enemy.gameObject.GetComponent<LightningHit>());
                    }
                }
            }
            
            Destroy(this.gameObject);
        }
    }

    // Get methods

    public List<Enemy> ChainedEnemy
    {
        get { return chainedEnemy; }
        set { chainedEnemy = value; }
    }

    public int Damage
    {
        set { damage = value; }
    }

    public int TargetAmount
    {
        get { return targetAmount; }
    }

    public int MaxTarget
    {
        get { return maxTarget; }
    }

    public float ChainTimer
    {
        get { return chainTimer; }
    }
}
