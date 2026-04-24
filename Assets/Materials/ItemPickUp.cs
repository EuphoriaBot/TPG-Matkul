using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ScoreManager scoreManagerScript;
    void Start()
    {
        scoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            scoreManagerScript.TotalScore += 5;

            Destroy(gameObject);
        }

    }
}
