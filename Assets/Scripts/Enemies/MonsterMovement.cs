using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private Animator anim;
    private int currentIdle;
    private float waitTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        PickNewDestination();
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed",0);

            PickRandomIdle();

            waitTimer += Time.deltaTime;

            if(waitTimer >= waitTime)
            {
                PickNewDestination();
                waitTimer = 0f;
            }
        }
    }

     void PickRandomIdle()
    {
        currentIdle = Random.Range(0,2);
        anim.SetInteger("IdleIndex", currentIdle);
    }

    private void PickNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere*wanderRadius;
        randomDirection += transform.position;

        if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
