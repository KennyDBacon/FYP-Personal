using UnityEngine;
using System.Collections;

public class Enemy : Unit
{

    protected enum AIState
    {
        Walk,
        Attack
    };

    protected NavMeshAgent navMeshAgent;
    protected Renderer renderer;
    protected Animator animator;
    protected AIState aiState;

    private bool isStopped = false;

    protected bool shuffleRight;
    protected float shuffleTime;
    protected float shuffleTimer = 0.0f;
    protected Vector3 lookAtVec;


	void Awake ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        SetState(AIState.Walk);

        shuffleRight = Random.Range(0, 2) == 1;
	}


    //PLEASE REMOVE ME KEVIN
    void OnDestroy ()
    {
        --GameManager.manager.enemyCount;
        GameManager.manager.AddShards(cost);
    }
    ////////

    protected virtual void Start()
    {
        if (target == null)
        {
            target = GameManager.manager.endPoint;
        }
        navMeshAgent.SetDestination(target.gameObject.transform.position);
    }

    protected override void Update()
    {
        if (aiState == AIState.Attack && !animator.IsInTransition(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            base.Update();
        }
        if (isStopped && aiState == AIState.Walk && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            isStopped = false;
            navMeshAgent.Resume();
        }
        if (target == null || !target.gameObject.activeSelf || (navMeshAgent.isPathStale && !navMeshAgent.pathPending))
        {
            ResetUnitAggro();
        }
        if (aiState == AIState.Attack && target != null)
        {
            lookAtVec.x = target.transform.position.x;
            lookAtVec.y = transform.position.y;
            lookAtVec.z = target.transform.position.z;
            transform.LookAt(lookAtVec);
            Shuffle();
            Attack();
        }
    }

    protected virtual void OnTriggerEnter (Collider col)
    {
        if (!col.isTrigger)
        {
            if (col.CompareTag("EndPoint") && target == GameManager.manager.endPoint)
            {
                if (aiState != AIState.Attack)
                {
                    SetState(AIState.Attack);
                }
                if (aiState == AIState.Attack)
                {
                    shuffleTime = Random.Range(-0.2f, 1.5f);
                    isStopped = true;
                    navMeshAgent.Stop();
                }
            }
        }
    }

    protected virtual void OnTriggerStay (Collider col)
    {
        if (!col.isTrigger && aiState != AIState.Attack)
        {
            OnTriggerEnter(col);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (!col.isTrigger && aiState == AIState.Attack)
        {
            if (col.gameObject == target.gameObject)
            {
                ResetUnitAggro();
            }
        }
    }

    protected virtual void ResetUnitAggro()
    {
        target = GameManager.manager.endPoint;
        SetState(AIState.Walk);
        shuffleTimer = 0.0f;
    }

    protected virtual void Shuffle()
    {
        if (shuffleTimer <= shuffleTime)
        {
            shuffleTimer += Time.deltaTime;
            if (shuffleRight)
            {
                navMeshAgent.Move(Vector3.right * Time.deltaTime * navMeshAgent.speed / 4);
            }
            else
            {
                navMeshAgent.Move(Vector3.left * Time.deltaTime * navMeshAgent.speed / 4);
            }
        }
    }

    protected void SetState(AIState aistate)
    {
        aiState = aistate;
        if(aiState == AIState.Walk)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    protected override void MeleeAttack()
    {
        animator.SetBool("Attack", true);
    }

    protected void Damage()
    {
        animator.SetBool("Attack", false);
        base.Damage();
    }

    public override void AliveCheck()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Die");
            navMeshAgent.Stop();
            GameManager.manager.AddShards(cost);
            GameManager.manager.enemyCount--;
        }
        else
        {
            animator.SetTrigger("Take Damage");
        }
    }


    //End-of-animation cue
    void EndOfDie()
    {
        Destroy(gameObject);
    }
}
