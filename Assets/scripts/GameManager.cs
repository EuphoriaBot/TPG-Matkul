using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int life = 5;
    public Transform SpawnPoint;
    public Transform Player;

    public bool IsGameOver { get; private set; } = false;

    void Start()
    {
        SpawnPlayer();
        UiController.Instance.HideGameOverPanel();
    }

    public IEnumerator Gameover()
    {
        life--;
        IsGameOver = true;

        UiController.Instance.ShowGameOverPanel(life);
        yield return new WaitForSeconds(2f);
        
        if (life <= 0)
        {
            Debug.Log("Game Over");
        }   else if (life > 0)
        {
            SpawnPlayer();
            UiController.Instance.HideGameOverPanel();
        }
    }
    public void SpawnPlayer()
    {
        IsGameOver = false;
        Player.position = SpawnPoint.position; 
        Player.GetComponent<CharacterController>().enabled = true;
    }

    private static GameManager s_instance;
    public static GameManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindFirstObjectByType<GameManager>();
            }
            return s_instance;
        }
    }


}
