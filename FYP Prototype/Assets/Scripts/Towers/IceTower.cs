﻿using UnityEngine;
using System.Collections;

public class IceTower : Tower {

    public GameObject iceParticle;

    private Vector3 moveDown;
    private Vector3 radiusVec;

    void Start()
    {
        //isRotating = true;
        moveDown = new Vector3(0, -Time.deltaTime * 4.2f, 0);
        radiusVec = new Vector3(Time.deltaTime * 1.2f, Time.deltaTime * 1.2f, Time.deltaTime * 1.2f);
        isDamaging = false;
    }

    void Update()
    {
        if (target != null)
        {
            AnimationControl();
            //TowerAction();

        }
        else
        {
            iceParticle.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.tag == "Enemy")
        {
            if (col.transform.root.GetComponent<Unit>() != null)
            {
                if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
                {
                    col.transform.root.GetComponent<NavMeshAgent>().speed *= 0.8f;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.root.tag == "Enemy")
        {
            if (col.transform.root.GetComponent<Unit>() != null)
            {
                if (!col.transform.root.GetComponent<Unit>().isAllyTeam)
                {
                    col.transform.root.GetComponent<NavMeshAgent>().speed *= 1.25f;
                }
            }
        }
    }

    private void AnimationControl()
    {
        if (iceParticle.GetComponent<ParticleSystem>().isStopped)
        {
            iceParticle.GetComponent<ParticleSystem>().Play();
        }

        iceParticle.transform.localPosition += moveDown;
        iceParticle.transform.localScale += radiusVec;
        //iceParticle.transform.Rotate(0, 0, 4.0f);

        if (iceParticle.transform.position.y <= -0.2f)
        {
            iceParticle.GetComponent<ParticleSystem>().Clear();
            iceParticle.transform.localScale = Vector3.one;
            iceParticle.transform.localPosition = Vector3.zero;
        }
    }
}
