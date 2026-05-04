using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Stuck detection
    private float stuckTimer = 0f;
    private float stuckTimeout = 0.1f;
    private float stuckThreshold = 0.05f; // min distance moved to not be considered stuck
    private Vector3 lastPosition;
    private int maxSearchAttempts = 10;

    public float sightRange;
    private bool playerInSightRange;

    void Awake()
    {
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange) Patroling();
        else ChasePlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            return;
        }

        agent.SetDestination(walkPoint);

        // Check if enemy has barely moved since last frame
        float movedDistance = Vector3.Distance(transform.position, lastPosition);
        if (movedDistance < stuckThreshold)
        {
            stuckTimer += Time.deltaTime;
        }
        else
        {
            stuckTimer = 0f; // reset if moving normally
        }

        lastPosition = transform.position;

        // Stuck for too long — abandon walkpoint
        if (stuckTimer >= stuckTimeout)
        {
            walkPointSet = false;
            stuckTimer = 0f;
            agent.ResetPath();
            return;
        }

        // Reached destination normally
        if (Vector3.Distance(transform.position, walkPoint) < 1f)
        {
            walkPointSet = false;
            stuckTimer = 0f;
        }
    }

    private void SearchWalkPoint()
    {
        for (int i = 0; i < maxSearchAttempts; i++)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            Vector3 candidate = new Vector3(
                transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ
            );

            if (!Physics.Raycast(candidate, Vector3.down, 2f, whatIsGround))
                continue;

            NavMeshHit hit;
            if (!NavMesh.SamplePosition(candidate, out hit, 1.5f, NavMesh.AllAreas))
                continue;

            NavMeshPath path = new NavMeshPath();
            if (!agent.CalculatePath(hit.position, path))
                continue;

            if (path.status != NavMeshPathStatus.PathComplete)
                continue;

            walkPoint = hit.position;
            walkPointSet = true;
            stuckTimer = 0f;
            return;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}