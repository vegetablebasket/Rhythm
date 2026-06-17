using UnityEngine;

public class TestObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public GameObject wallObstaclePrefab;
    public GameObject jumpObstaclePrefab;

    [Header("Spawn Settings")]
    public float spawnSpeed = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnWall(ObstaclePattern.MiddleGap);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnWall(ObstaclePattern.LeftGap);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnWall(ObstaclePattern.RightGap);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnJump();
        }
    }

    private void SpawnWall(ObstaclePattern pattern)
    {
        if (wallObstaclePrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("缺少 WallObstaclePrefab 或 SpawnPoint");
            return;
        }

        GameObject obstacle = Instantiate(
            wallObstaclePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        WallObstacle wall = obstacle.GetComponent<WallObstacle>();
        if (wall != null)
        {
            wall.pattern = pattern;
        }

        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
        if (mover != null)
        {
            mover.SetSpeed(spawnSpeed);
        }
    }

    private void SpawnJump()
    {
        if (jumpObstaclePrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("缺少 JumpObstaclePrefab 或 SpawnPoint");
            return;
        }

        GameObject obstacle = Instantiate(
            jumpObstaclePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
        if (mover != null)
        {
            mover.SetSpeed(spawnSpeed);
        }
    }
}
