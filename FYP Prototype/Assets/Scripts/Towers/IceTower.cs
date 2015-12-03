using UnityEngine;
using System.Collections;

public class IceTower : Tower {

    public GameObject iceParticle;

    void Start()
    {
        isRotating = true;
        isDamaging = false;
        isUpgradable = true;
    }
    
    void Update () {
        if (target != null)
        {
            TowerAction();

            iceParticle.GetComponent<ParticleSystem>().Play();

            if (attackTimer >= unit.attackInterval)
            {
                attackTimer = 0;
            }
        }
        else
        {
            iceParticle.GetComponent<ParticleSystem>().Stop();
        }
    }
}
