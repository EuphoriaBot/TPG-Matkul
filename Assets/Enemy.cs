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

    private Vector3 SpawnPoint;

    private void Awake()
    {
        player = GameObject.Find("First Person Player").transform;
        agent = GetComponent<NavMeshAgent>();
        SpawnPoint = transform.position;
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange) Patroling();
        else if (playerInSightRange) chaseplayer();

        agent.enabled = !GameManager.Instance.IsGameOver;

        Fase(); 
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        } 
        
        if (walkPointSet && agent.enabled)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        } 
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void chaseplayer()
    {
        if (!agent.enabled) return;
        agent.SetDestination(player.position);
    }

    private void Fase()
    {
        int score = ScoreManager.Instance.TotalScore;

        if (score >= 100)
        {
            agent.speed = 5f;
        }
        else if (score >= 50)
        {
            agent.speed = 3.5f;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");
            other.gameObject.GetComponent<PlayerController>().die();
            transform.position = SpawnPoint;
        }
    }
}
   
