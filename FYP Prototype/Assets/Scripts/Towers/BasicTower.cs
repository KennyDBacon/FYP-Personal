using UnityEngine;
using System.Collections;

public class BasicTower : Tower {
    
    void Update()
    {
        if (target != null)
        {
            TowerAction();
        }
    }
}
