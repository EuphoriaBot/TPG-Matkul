using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int totalScore;
    public int TotalScore
    {
        get => totalScore;
        set
        {
            totalScore = value;
            onScoreChanged?.Invoke();
        }
    }
    public Action onScoreChanged;
    private static ScoreManager s_instance;
    public static ScoreManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindFirstObjectByType<ScoreManager>();
            }
            return s_instance;
        }
    }

}
