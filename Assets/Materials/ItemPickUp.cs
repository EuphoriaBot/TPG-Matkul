using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ScoreManager scoreManagerScript => ScoreManager.Instance;
    public AudioSource crystalSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManagerScript.AddScore(5);

            AudioSource.PlayClipAtPoint(crystalSound.clip, transform.position);

            Destroy(gameObject);
        }
    }
}