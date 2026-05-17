using JetBrains.Annotations;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   public EnemyAI ai;

   public Transform player;   

    public Transform SpawnPoint;

    private float searchCooldown = 0f;
    private float searchInterval = 0.5f; 

   void Awake()
    {
        ai = GetComponent<EnemyAI>();
        player = GameObject.Find("First Person Player").transform;
    }

    public void Start()
    {
        ScoreManager.Instance.OnScoreUpdated += Fase;
    }

   
    private void Fase()
    {
        switch(ScoreManager.Instance.TotalScore)
        {
            case 200:
                ai.agent.speed += 0.5f;
                break;
            case 150:
                ai.agent.speed += 1f;
                break;
            case 100:
                ai.agent.speed += 0.5f;
                break;
            case 50:
                ai.agent.speed += 1f;
                break;
            case 10:
                ai.agent.speed += 1f;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");

            ai.agent.enabled = false;
            transform.position = SpawnPoint.position;
            ai.agent.enabled = true;
            other.gameObject.GetComponent<PlayerController>().die();
            ai.walkPointSet = false;
        }
    }
}
   
