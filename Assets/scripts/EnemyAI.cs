using System;
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
    private bool _playerInSightRange;
    public bool playerInSightRange
    {
        get { return _playerInSightRange; }
        set
        {
            if (_playerInSightRange != value)
            {
                _playerInSightRange = value;
                OnPlayerSpotted?.Invoke();
            }
        }
    }

    public Action OnPlayerSpotted;

    void Awake()
    {
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
    }

    void Start()
    {
        OnPlayerSpotted += BacksoundManager.Instance.EnemyBacksound;
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
            Vector3 randomPoint = new Vector3(
                UnityEngine.Random.Range(-100f, 100f),
                transform.position.y,
                UnityEngine.Random.Range(-100f, 100f)
            );

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 20f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();

                if (agent.CalculatePath(hit.position, path) &&
                    path.status == NavMeshPathStatus.PathComplete)
                {
                    walkPoint = hit.position;
                    walkPointSet = true;
                    return;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}