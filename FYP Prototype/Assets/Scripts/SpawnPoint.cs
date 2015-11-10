using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{

    public GameObject normalEnemy;
    public GameObject specialEnemy;
    public float spawnInterval = 1.5f;

    void Start ()
    {
        InvokeRepeating("SpawnMonster", spawnInterval, spawnInterval);
    }

	void SpawnMonster ()
    {
        if (Random.Range (0.0f,1.0f) >= 0.8)
        {
            Instantiate (specialEnemy, transform.position, Quaternion.identity);
        } else
        {
            Instantiate (normalEnemy, transform.position, Quaternion.identity);
        }
	}
}
