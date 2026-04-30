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

            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = transform.position;
            AudioSource aSource = tempGO.AddComponent<AudioSource>();

            aSource.clip = crystalSound.clip;
            aSource.pitch = Random.Range(0.9f, 1.1f);

            aSource.Play();
            Destroy(tempGO, crystalSound.clip.length);

            Destroy(gameObject);
        }
    }
}