using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   public NavMeshAgent agent;

   public Transform player;

   public LayerMask whatIsGround, whatIsPlayer;

    //Patroling

    public Vector3 walkPoint;

    bool walkPointSet;

    public float walkPointRange;

    //states
    public float sightRange;
    public bool playerInSightRange;

    public Transform SpawnPoint;

    private float searchCooldown = 0f;
    private float searchInterval = 0.5f; // cari walkpoint setiap 0.5 detik

    private void Awake()
    {
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        // SpawnPoint = transform.position;
        ScoreManager.Instance.OnScoreUpdated += Fase;
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange) Patroling();
        else if (playerInSightRange) chaseplayer();

        // agent.enabled = !GameManager.Instance.IsGameOver; 
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            searchCooldown -= Time.deltaTime;
            if (searchCooldown <= 0f)
            {
                SearchWalkPoint();
                searchCooldown = searchInterval;
            }
        }

        if (walkPointSet && agent.enabled)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 randomPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, walkPointRange, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
            Debug.Log("Walkpoint ditemukan: " + walkPoint);
        }
        else
        {
            Debug.Log("NavMesh sample gagal");
        }
    }
    private void chaseplayer()
    {
        if (!agent.enabled) return;
        agent.SetDestination(player.position);
    }

    private void Fase()
    {
        switch(ScoreManager.Instance.TotalScore)
        {
            case 50:
                agent.speed += 0.5f;
                break;
            case 100:
                agent.speed += 1f;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");

            agent.enabled = false;
            transform.position = SpawnPoint.position;
            agent.enabled = true;
            other.gameObject.GetComponent<PlayerController>().die();
            walkPointSet = false;
        }
    }
}
   
