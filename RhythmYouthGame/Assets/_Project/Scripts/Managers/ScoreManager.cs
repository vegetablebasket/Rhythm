using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score Settings")]
    public int goodScore = 100;

    [Header("Runtime Data")]
    public int score = 0;
    public int combo = 0;
    public int maxCombo = 0;
    public int goodCount = 0;
    public int missCount = 0;
    public int totalObstacleCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        RefreshTotalObstacleCount();
    }

    public void AddGood()
    {
        score += goodScore;
        combo += 1;
        goodCount += 1;

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
            UIManager.Instance.UpdateCombo(combo);
        }

        Debug.Log($"Good! Score: {score}, Combo: {combo}, MaxCombo: {maxCombo}");
    }

    public void AddMiss()
    {
        combo = 0;
        missCount += 1;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateCombo(combo);
        }

        Debug.Log($"Miss! Combo reset. MissCount: {missCount}");
    }

    public float GetHitRate()
    {
        if (totalObstacleCount <= 0)
        {
            return 0f;
        }

        return (float)goodCount / totalObstacleCount * 100f;
    }

    public void ResetScore()
    {
        score = 0;
        combo = 0;
        maxCombo = 0;
        goodCount = 0;
        missCount = 0;

        RefreshTotalObstacleCount();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
            UIManager.Instance.UpdateCombo(combo);
        }

        Debug.Log("ScoreManager reset.");
    }

    private void RefreshTotalObstacleCount()
    {
        BeatMapManager beatMapManager = FindObjectOfType<BeatMapManager>();
        if (beatMapManager != null)
        {
            totalObstacleCount = beatMapManager.GetTotalObstacleCount();
        }
    }
}
