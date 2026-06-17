using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public AudioManager audioManager;
    public BeatMapManager beatMapManager;
    public Transform spawnPoint;

    [Header("Prefabs")]
    public GameObject wallObstaclePrefab;
    public GameObject jumpObstaclePrefab;

    [Header("Runtime")]
    public bool canSpawn = true;

    private readonly List<GameObject> spawnedObstacles = new List<GameObject>();

    private void Update()
    {
        if (!canSpawn)
        {
            return;
        }

        if (audioManager == null || beatMapManager == null)
        {
            return;
        }

        if (!audioManager.IsMusicPlaying())
        {
            return;
        }

        UpdateSpawn();
    }

    private void UpdateSpawn()
    {
        float musicTime = audioManager.GetMusicTime();
        List<BeatEvent> beatEvents = beatMapManager.GetBeatEvents();

        foreach (BeatEvent beatEvent in beatEvents)
        {
            if (beatEvent.spawned)
            {
                continue;
            }

            if (musicTime >= beatEvent.time)
            {
                SpawnObstacle(beatEvent);
                beatEvent.spawned = true;
            }
        }
    }

    private void SpawnObstacle(BeatEvent beatEvent)
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("ObstacleSpawner 缺少 SpawnPoint");
            return;
        }

        GameObject prefab = GetPrefabByType(beatEvent.type);

        if (prefab == null)
        {
            Debug.LogWarning("ObstacleSpawner 缺少对应类型的障碍 Prefab：" + beatEvent.type);
            return;
        }

        GameObject obstacle = Instantiate(
            prefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        obstacle.name = $"Obstacle_{beatEvent.id}_{beatEvent.type}_{beatEvent.pattern}";

        ObstacleBase obstacleBase = obstacle.GetComponent<ObstacleBase>();
        if (obstacleBase != null)
        {
            obstacleBase.Init(beatEvent.id, beatEvent.type, beatEvent.pattern);
        }

        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
        if (mover != null)
        {
            mover.SetSpeed(beatEvent.speed);
        }

        WallObstacle wallObstacle = obstacle.GetComponent<WallObstacle>();
        if (wallObstacle != null && beatEvent.type == ObstacleType.Wall)
        {
            wallObstacle.SetPattern(beatEvent.pattern);
        }

        spawnedObstacles.Add(obstacle);
    }

    private GameObject GetPrefabByType(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.Wall:
                return wallObstaclePrefab;

            case ObstacleType.Jump:
                return jumpObstaclePrefab;

            default:
                return null;
        }
    }

    public void ClearObstacles()
    {
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            if (spawnedObstacles[i] != null)
            {
                Destroy(spawnedObstacles[i]);
            }
        }

        spawnedObstacles.Clear();
    }

    public void ResetSpawner()
    {
        ClearObstacles();

        if (beatMapManager != null)
        {
            beatMapManager.ResetBeatMap();
        }
    }

    public void SetSpawnEnabled(bool enabled)
    {
        canSpawn = enabled;
    }
}
