using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ScoreManager scoreManagerScript;
    void Start()
    {
        scoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            scoreManagerScript.totalScore += 5;

            Destroy(gameObject);
        }

    }
}
