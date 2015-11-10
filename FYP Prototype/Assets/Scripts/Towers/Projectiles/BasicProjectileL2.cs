using UnityEngine;
using System.Collections;

public class BasicProjectileL2 : TowerProjectile {

    public GameObject model;

    private GameObject projectileA, projectileB;

    void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = false;

        projectileA = Instantiate(model, transform.position + Vector3.left, Quaternion.identity) as GameObject;
        projectileB = Instantiate(model, transform.position + Vector3.right, Quaternion.identity) as GameObject;
    }

	// Update is called once per frame
	void Update () {
        gameObject.transform.LookAt(target.gameObject.transform);
        transform.position += speed * transform.forward * Time.deltaTime;

        projectileA.transform.position += speed * transform.forward * Time.deltaTime;
        projectileB.transform.position += speed * transform.forward * Time.deltaTime;

        transform.RotateAround(projectileA.transform.position, Vector3.up, 10 * Time.deltaTime);
        transform.RotateAround(projectileB.transform.position, Vector3.up, 10 * Time.deltaTime);
    }
}
