using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public List<GameObject> GameOverPanels;
    public TMP_Text ScoreText;
    public GameObject ScorePanel;

    public GameObject TutorialPanel;
    public GameObject PausePanel;
    public GameObject ObjectivePanel;
    public TMP_Text ObjectiveText;

    public void ShowGameOverPanel(int life)
    {
        GameOverPanels[math.clamp(life, 0, GameOverPanels.Count - 1)].SetActive(true);
    }

    public void HideGameOverPanel()
    {
        foreach (var panel in GameOverPanels)
        {
            panel.SetActive(false);
        }
    }
    
    public void ShowTutorial()
    {
        TutorialPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HideTutorial()
    {
        TutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void SetScore(int score)
    {
        ScoreText.SetText(score.ToString());
    }

    public void ShowScorePanel(bool show)
    {
        ScorePanel.SetActive(show);
    }

    public void ShowPausePanel(bool show)
    {
        PausePanel.SetActive(show);
    }

    public void ShowObjectivePanel(bool show)
    {
        ObjectivePanel.SetActive(show);
    }

    public void SetObjectiveText(string text)
    {
        ObjectiveText.SetText(text);
    }

    public void HideObjectivePanel()
    {
        ObjectivePanel.SetActive(false);
    }

    private static UiController s_instance;
    public static UiController Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindFirstObjectByType<UiController>();
            }
            return s_instance;
        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.TogglePause();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
