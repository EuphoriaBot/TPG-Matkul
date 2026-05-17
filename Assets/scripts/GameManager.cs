using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int life = 5;
    public Vector3 SpawnPos;
    public Transform Player;

    public bool IsGameOver { get; private set; } = false;

    public Transform PointerPoint;

    public int StartCrystalHint = 30;

    public bool IsPaused { get; private set; } = false;

    void Awake()
    {
        SpawnPos = Player.position;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SpawnPlayer();
        UiController.Instance.HideGameOverPanel();
        UiController.Instance.ShowPausePanel(false);
        UiController.Instance.ShowTutorial();
        ScoreManager.Instance.OnScoreUpdated += () => {
            if (ScoreManager.Instance.TotalScore == 0)
            {
                UiController.Instance.SetObjectiveText("- Kristal telah habis, cari portal keluar!");
            }
        };
    }

    void Update()
    {
        if (!IsGameOver && !IsPaused)
        {
            GetNearestCrystal();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleObjectivePanel();
        }
    }

    public void GetNearestCrystal()
    {
        ItemPickUp[] itemPickUps = GetAllItemPickups();
        if (itemPickUps.Length > StartCrystalHint)
        {
            PointerPoint = null;
            return;
        }
        if (itemPickUps.Length == 0)
        {
            PointerPoint = FindFirstObjectByType<Portal>()?.transform;
            return;
        }
        foreach (ItemPickUp item in itemPickUps)
        {
            if (PointerPoint == null || Vector3.Distance(Player.position, item.transform.position) < Vector3.Distance(Player.position, PointerPoint.position))
            {
                PointerPoint = item.transform;
            }
        }
    }

    public IEnumerator Gameover()
    {
        life--;
        IsGameOver = true;

        UiController.Instance.ShowGameOverPanel(life);
        yield return new WaitForSeconds(2f);

        if (life <= 0)
        {
            UiController.Instance.ShowScorePanel(false);
            UiController.Instance.ShowObjectivePanel(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;

            Debug.Log("Game Over");
        }
        else if (life > 0)
        {
            SpawnPlayer();
            UiController.Instance.HideGameOverPanel();
        }
    }

    public void SpawnPlayer()
    {
        IsGameOver = false;
        Player.position = SpawnPos;
        Player.GetComponent<CharacterController>().enabled = true;

        foreach (Enemy enemy in GetAllEnemies())
        {
            enemy.ResetToSpawnPoint();
        }
    }

    public Enemy[] GetAllEnemies()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None);
    }

    public ItemPickUp[] GetAllItemPickups()
    {
        return FindObjectsByType<ItemPickUp>(FindObjectsSortMode.None);
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0f : 1f;
        UiController.Instance.ShowPausePanel(IsPaused);

        Cursor.visible = IsPaused;
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;

        Debug.Log("IsPaused: " + IsPaused + " | Cursor visible: " + Cursor.visible + " | LockState: " + Cursor.lockState);
    }

    public void ToggleObjectivePanel()
    {
        bool isActive = UiController.Instance.ObjectivePanel.activeSelf;
        UiController.Instance.ShowObjectivePanel(!isActive);
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