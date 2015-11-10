using UnityEngine;
using System.Collections;

public class TowerBuilding : MonoBehaviour {

    private bool isBuildable = true;
    
    void OnTriggerEnter(Collider col)
    {
        if(col.GetType() != typeof(SphereCollider))
        {
            isBuildable = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.GetType() != typeof(SphereCollider))
        {
            isBuildable = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        isBuildable = true;
    }

    public bool IsBuildable
    {
        get { return isBuildable; }
    }
}
