using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _TotalScore = 0;

    public int TotalScore
    {
        get => _TotalScore;
        set
        {
            _TotalScore = value;
            OnScoreUpdated?.Invoke();
            UiController.Instance.SetScore(_TotalScore);
        }
    }

    public Action OnScoreUpdated;

    void Start()
    {
        TotalScore = 0;
    }

    public void AddScore(int amount)
    {
        TotalScore += amount;
        Debug.Log("Score sekarang: " + TotalScore);
    }

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