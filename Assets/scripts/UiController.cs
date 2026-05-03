using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public List<GameObject> GameOverPanels;
    public TMP_Text ScoreText;
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
    
    public void SetScore(int score)
    {
        ScoreText.SetText(score.ToString());
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
}
