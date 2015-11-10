using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flamethrower : MonoBehaviour {
    List<Unit> enemyHit;

	void Start () {
        enemyHit = new List<Unit>();
	}
	
	void Update () {
	    for(int i = 0; i < enemyHit.Count - 1; --i)
        {
            enemyHit.RemoveAt(i);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Enemy>() != null)
        {
            enemyHit.Add(col.gameObject.GetComponent<Unit>());
        }
    }

    void OnTriggerExit(Collider col)
    {
        enemyHit.Remove(col.gameObject.GetComponent<Unit>());
    }

    public List<Unit> GetEnemyList
    {
        get { return enemyHit; }
    }
}
