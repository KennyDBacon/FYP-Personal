﻿using UnityEngine;
using System.Collections;
using System;

public class Tower : MonoBehaviour {

    public Transform turret;
    public Boolean isRotating = false;

    protected Unit target = null;
    protected Unit unit;

    public bool isDamaging = true;
    protected float attackTimer;

    void Awake ()
    {
        unit = GetComponent<Unit>();

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
        if(isRotating && turret != null)
        {
            //RotateTurret();
            turret.LookAt(target.gameObject.transform);
        }

        if (isDamaging)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= unit.attackInterval)
            {
                //unit.projectile.GetComponent<TowerProjectile>().damage = unit.damage;
            }
            //unit.Attack();
        }
    }

    void OnTriggerEnter (Collider col)
    {
        FindTarget(col);
    }

    void OnTriggerStay (Collider col)
    {
        FindTarget(col);
    }

    void OnTriggerExit (Collider col)
    {
        if (target != null && !col.isTrigger && col.gameObject == target.gameObject)
        {
            target = null;
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

    // Special function

    // Rotate the turret along Y-axis, so the tower doesn't look down toward the target
    protected void RotateTurret()
    {
        //Vector3 rot = new Vector3(target.transform.position.x,
        //                          turret.transform.position.y,
        //                          target.transform.position.z);

        Vector3 rot = target.transform.position - turret.transform.position;
        //rot.y = 0;
        Quaternion lookDir = Quaternion.LookRotation(rot);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, lookDir, Time.deltaTime * 4);
    }
}
