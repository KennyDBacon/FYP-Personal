using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthTower : Tower {

    public GameObject stonePrefab;
    private List<GameObject> bunchOfFloatingStones;
    public int rockAmount = 5;

    private Transform rotaryCenter;

    private float attackTimer;
    private float stoneTimer = 0;
    private float stoneMaxSize = 0.2f;

	void Start () {
        bunchOfFloatingStones = new List<GameObject>();
        rotaryCenter = transform.FindChild("Center");

        float angleSpacing = (Mathf.PI * 2) / rockAmount;
        float radius = 0.6f;
        for (int i = 0; i < rockAmount; i++)
        {
            float posX = Mathf.Sin(i * angleSpacing) * radius;
            float posZ = Mathf.Cos(i * angleSpacing) * radius;

            Vector3 newPos = new Vector3(posX, 0, posZ);
            GameObject stone = Instantiate(stonePrefab, newPos, Quaternion.identity) as GameObject;
            stone.transform.parent = rotaryCenter.transform;
            stone.transform.localPosition = newPos;
            stone.transform.localRotation = rotaryCenter.transform.rotation = Quaternion.Euler(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45));
            stone.transform.localScale = new Vector3(stoneMaxSize, stoneMaxSize, stoneMaxSize);

            bunchOfFloatingStones.Add(stone);
        }

        rotaryCenter.transform.rotation = Quaternion.Euler(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45));
	}
	
	void Update () {
        rotaryCenter.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));

        foreach(GameObject stone in bunchOfFloatingStones)
        {
            if(stone.transform.localScale.x < stoneMaxSize)
            {
                stone.transform.localScale += new Vector3(Time.deltaTime / 4, Time.deltaTime / 4, Time.deltaTime / 4);
                stone.transform.Rotate(new Vector3(Time.deltaTime * 300, 0, 0));
            }
        }

        attackTimer += Time.deltaTime;
        if (target != null)
        {
            if (attackTimer >= unit.attackInterval)
            {
                attackTimer = 0;
                FireAtTarget();
            }
        }
	}

    void FireAtTarget()
    {
        int index = Random.Range(0, bunchOfFloatingStones.Count);
        bunchOfFloatingStones[index].transform.localScale = new Vector3(0, 0, 0);
        GameObject flyingStone = Instantiate(unit.projectile, bunchOfFloatingStones[index].transform.position, Quaternion.identity) as GameObject;
        flyingStone.transform.rotation = bunchOfFloatingStones[index].transform.rotation;
        flyingStone.GetComponent<TowerProjectile>().target = target;
        //flyingStone.GetComponent<TowerProjectile>().damage = unit.damage;
    }
}
