using UnityEngine;
using System.Collections;

public class FireTower : Tower {

    public GameObject crystals;
    private float crystalSpeed = 0;
    private float maxSpeed = 600;
    private float rotationSpeed = 150;

    public Transform barrel;
    public Transform center;
    public GameObject fireParticle;

    //void Start()
    //{
    //    isRotating = true;
    //}

	void Update () {
        AnimationControl();

	    if(target != null)
        {
            TowerAction();

            RotateTurret();
            barrel.LookAt(target.gameObject.transform);

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

    // Custom animation with code because the animator won't work when the object is moved
    private void AnimationControl()
    {
        if (target != null)
        {
            if (crystalSpeed <= maxSpeed)
            {
                crystalSpeed += Time.deltaTime * rotationSpeed;
            }

        }
        else
        {
            if (crystalSpeed > 0)
            {
                crystalSpeed -= Time.deltaTime * rotationSpeed;
            }
        }

        if(crystalSpeed > 0)
            crystals.transform.Rotate(0, Time.deltaTime * crystalSpeed, 0);
    }

    // Rotate the turret along Y-axis, so the tower doesn't look down toward the target
    private void RotateTurret()
    {
        //Vector3 rot = new Vector3(target.transform.position.x,
        //                          turret.transform.position.y,
        //                          target.transform.position.z);

        Vector3 rot = target.transform.position - center.transform.position;
        rot.y = 0;
        Quaternion lookDir = Quaternion.LookRotation(rot);
        center.rotation = Quaternion.Slerp(center.transform.rotation, lookDir, Time.deltaTime * 8);
    }
}
