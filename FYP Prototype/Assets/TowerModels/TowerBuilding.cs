using UnityEngine;
using System.Collections;

public class TowerBuilding : MonoBehaviour {

    private bool isBuildable = true;
    private Color originalMat;
    private Color transparentMat;

    void Start()
    {
        originalMat = gameObject.GetComponent<MeshRenderer>().materials[0].color;
        transparentMat = new Color(originalMat.r, originalMat.g, originalMat.b, 0.5f);
    }

    void Update()
    {
        if (!isBuildable)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = transparentMat;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = originalMat;
        }
    }
    
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
