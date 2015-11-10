using UnityEngine;
using System.Collections;

public class IceTower : Tower {

    void Start()
    {
        isRotating = true;
    }
    
    void Update () {
        if (target != null)
        {
            TowerAction();
        }

        //Debug.DrawLine(unit.GetProjectileSpawnPoint.position, target.transform.position, Color.blue);
    }
}
