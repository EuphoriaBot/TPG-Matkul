using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalVisual; // portal yang awalnya mati/tertutup
    public GameObject congratulationsText; // UI tulisan congratulations

    private bool portalOpen = false;

    void Start()
    {
        // portal awalnya tertutup
        portalVisual.SetActive(false);

        // tulisan awalnya tidak muncul
        congratulationsText.SetActive(false);

        // dengarkan perubahan score
        ScoreManager.Instance.OnScoreUpdated += CheckPortal;
    }

    void CheckPortal()
    {
        if (ScoreManager.Instance.TotalScore <= 0)
        {
            OpenPortal();
        }
    }

    void OpenPortal()
    {
        portalOpen = true;

        // munculkan portal
        portalVisual.SetActive(true);

        Debug.Log("Portal terbuka!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (portalOpen && other.CompareTag("Player"))
        {
            congratulationsText.SetActive(true);

            Debug.Log("Congratulations!");
        }
    }
}

