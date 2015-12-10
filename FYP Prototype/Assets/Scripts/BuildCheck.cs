using UnityEngine;
using System.Collections;

public class BuildCheck : MonoBehaviour
{

    private Renderer[] renderers;

    public bool isBuildable = true;
    private bool isColliding = false;
    private bool isBlockingPath = false;
    private bool prevIsBuildable = true;
    private Color tempColor = new Color(0, 255, 0);
    private NavMeshPath navMeshPath;

    void Awake ()
    {
        renderers = GetComponentsInChildren<Renderer>();
        navMeshPath = new NavMeshPath();
    }

    public void Check()
    {
        isBlockingPath = false;
        foreach (SpawnPoint tempSpawn in GameManager.manager.spawnPoints)
        {
            NavMesh.CalculatePath(tempSpawn.transform.position, GameManager.manager.endPoint.transform.position, NavMesh.AllAreas, navMeshPath);
            if (navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                isBlockingPath = true;
                break;
            }
        }
        isBuildable = !isColliding && !isBlockingPath;
        if (prevIsBuildable != isBuildable)
        {
            prevIsBuildable = isBuildable;
            ChangeColor();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        OnTriggerStay(col);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.GetType() != typeof(SphereCollider) && !col.CompareTag("Terrain"))
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetType() != typeof(SphereCollider) && !col.CompareTag("Terrain"))
        {
            isColliding = false;
        }
    }

    void ChangeColor()
    {
        if (isBuildable)
        {
            tempColor.r = 0;
            tempColor.g = 255;
        }
        else
        {
            tempColor.r = 255;
            tempColor.g = 0;
        }
        foreach (Renderer tempRenderer in renderers)
        {
            foreach (Material tempMat in tempRenderer.materials)
            {
                tempMat.color = tempColor;
            }
        }
    }
}
