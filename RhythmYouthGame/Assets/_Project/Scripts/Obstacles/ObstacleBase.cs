using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [Header("Obstacle Info")]
    public int obstacleId;
    public ObstacleType obstacleType;
    public ObstaclePattern pattern;

    [Header("Judge Settings")]
    public float passZ = -1f;
    public bool hasJudged = false;

    private void Update()
    {
        CheckPassedPlayer();
    }

    public void Init(int id, ObstacleType type, ObstaclePattern obstaclePattern)
    {
        obstacleId = id;
        obstacleType = type;
        pattern = obstaclePattern;
        hasJudged = false;
    }

    private void CheckPassedPlayer()
    {
        if (hasJudged)
        {
            return;
        }

        if (transform.position.z <= passZ)
        {
            HandleGood();
        }
    }

    public void HandlePlayerHit()
    {
        if (hasJudged)
        {
            return;
        }

        hasJudged = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddMiss();
        }

        if (HealthManager.Instance != null)
        {
            HealthManager.Instance.TakeDamage(1);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowFeedback("Miss");
        }

        Debug.Log($"Obstacle {obstacleId} Miss: {obstacleType}, {pattern}");

        Destroy(gameObject);
    }

    private void HandleGood()
    {
        if (hasJudged)
        {
            return;
        }

        hasJudged = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddGood();
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowFeedback("Good");
        }

        Debug.Log($"Obstacle {obstacleId} Good: {obstacleType}, {pattern}");

        Destroy(gameObject);
    }
}