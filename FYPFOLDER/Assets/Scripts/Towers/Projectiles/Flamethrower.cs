using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flamethrower : MonoBehaviour {
    List<Unit> enemyHit;

	void Start () {
        enemyHit = new List<Unit>();
	}
	
	void Update () {
        //for(int i = 0; i < enemyHit.Count - 1; --i)
        //{
        //    enemyHit.RemoveAt(i);
        //}
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.root.gameObject.GetComponent<Enemy>() != null)
        {
            enemyHit.Add(col.gameObject.transform.root.GetComponent<Unit>());
        }
    }

    void OnTriggerExit(Collider col)
    {
        enemyHit.Remove(col.gameObject.transform.root.GetComponent<Unit>());
    }

    public List<Unit> GetEnemyList
    {
        get { return enemyHit; }
    }
}
