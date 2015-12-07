using UnityEngine;
using System.Collections;
using System;

public class Tower : MonoBehaviour {

    //public Boolean isRotating = false;

    protected Unit target = null;
    protected Unit unit;
    protected SphereCollider sphereCol;

    public bool isDamaging = true;
    protected float attackTimer;

    void Awake ()
    {
        unit = GetComponent<Unit>();
        sphereCol = GetComponent<SphereCollider>();

        attackTimer = unit.attackInterval;
    }

	void Update () 
    {
        if (target != null)
        {
            TowerAction();
        }
    }

    protected void TowerAction()
    {
        //if(isRotating && turret != null)
        //{
        //    //RotateTurret();
        //}

        if (target != null)
        {
            if (isDamaging)
            {
                attackTimer += Time.deltaTime;
            }
        }
        else
        {
            sphereCol.radius = 0.1f;
            sphereCol.radius = 6.0f;
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Enemy")
        {
            FindTarget(col);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("Stay");
            FindTarget(col);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("Exit");
            if (target != null && !col.isTrigger && col.gameObject == target.gameObject)
            {
                target = null;
            }
        }
    }

    // Replacing unit Attack since it broke
    void TowerAttack()
    {

    }

    void FindTarget (Collider col)
    {
        if (target == null && col.transform.root.GetComponent<Unit>() != null)
        {
            if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
            {
                target = col.transform.root.GetComponent<Unit>();
            }
        }
    }
}
