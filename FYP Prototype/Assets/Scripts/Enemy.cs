using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private enum AIState
    {
        Walk,
        Attack
    };

    private NavMeshAgent navMeshAgent;
    private Renderer renderer;
    private AIState aiState;
    private Unit unit;
    private Unit target = null;


	void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        aiState = AIState.Walk;
        unit = GetComponent<Unit>();
	}

    void Start ()
    {
        if (GameManager.endPoint != null) {
            target = GameManager.endPoint.GetComponent<Unit>();
            navMeshAgent.destination = target.gameObject.transform.position;
        }
    }

    void Update()
    {
        if (aiState == AIState.Attack) {
            if (target != null) {
                unit.Attack();
            }
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "EndPoint")
        {
            //unit.Stop();
            aiState = AIState.Attack;
            navMeshAgent.destination = gameObject.transform.position;
            renderer.material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "EndPoint")
        {
            //unit.Stop();
            aiState = AIState.Walk;
            navMeshAgent.destination = target.gameObject.transform.position;
            renderer.material.color = Color.black;
        }
    }
}
