using UnityEngine;
using System.Collections;

public class Hunter : Enemy
{
    protected override void Start()
    {
	    if (GameManager.manager.player.gameObject.activeSelf)
        {
            target = GameManager.manager.player;
        }
        base.Start();
	}
	
	protected override void Update () 
    {
        if (GameManager.manager.player.gameObject.activeSelf)
        {
            if (target != GameManager.manager.player)
            {
                ResetUnitAggro();
            }
            if (target == GameManager.manager.player)
            {
                if (aiState != AIState.Attack && Vector3.Distance(target.gameObject.transform.position, navMeshAgent.destination) > 0.6f)
                {
                    navMeshAgent.SetDestination(target.gameObject.transform.position);
                }
            }
        }
        else if (target != GameManager.manager.endPoint)
        {
            ResetUnitAggro();
            navMeshAgent.SetDestination(target.transform.position);
        }
        base.Update();
	}

    protected override void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger && aiState != AIState.Attack)
        {
            if (col.CompareTag("Player"))
            {
                SetState(AIState.Attack);
            }
            base.OnTriggerEnter(col);
        }
    }

    protected override void ResetUnitAggro()
    {
        base.ResetUnitAggro();
        if (GameManager.manager.player.gameObject.activeSelf)
        {
            target = GameManager.manager.player;
        }
    }

    protected override void Shuffle()
    {
        if(target.CompareTag("Player"))
        {
            if (shuffleTimer <= shuffleTime)
            {
                shuffleTimer += Time.deltaTime;
                if (shuffleRight)
                {
                    navMeshAgent.Move(transform.right * Time.deltaTime * navMeshAgent.speed / 4);
                }
                else
                {
                    navMeshAgent.Move(-transform.right * Time.deltaTime * navMeshAgent.speed / 4);
                }
            }
        }
        else
        {
            base.Shuffle();
        }
    }
}
