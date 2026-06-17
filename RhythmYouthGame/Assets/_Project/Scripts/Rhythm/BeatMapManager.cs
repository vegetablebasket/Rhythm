using System.Collections.Generic;
using UnityEngine;

public class BeatMapManager : MonoBehaviour
{
    [Header("Beat Events")]
    public List<BeatEvent> beatEvents = new List<BeatEvent>();

    [Header("Timing Settings")]
    [Tooltip("障碍从生成点移动到玩家面前的大约时间。SpawnPoint Z=12、Speed=6 时约为 2 秒。")]
    public float obstacleLeadTime = 2f;

    [Header("Demo Settings")]
    public bool autoBuildIfEmpty = true;

    private void Reset()
    {
        BuildDemoBeatMap();
    }

    private void Awake()
    {
        if (autoBuildIfEmpty && beatEvents.Count == 0)
        {
            BuildDemoBeatMap();
        }

        ResetBeatMap();
    }

    [ContextMenu("Build Demo Beat Map")]
[ContextMenu("Build Demo Beat Map")]
    [ContextMenu("Build Demo Beat Map")]
    public void BuildDemoBeatMap()
    {
        beatEvents.Clear();

        int id = 1;

        // 第一段：10s - 30s，适应段，基本 1.5 秒一个障碍
        AddBeat(id++, 10.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 11.56f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 13.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 14.56f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 16.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 17.56f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 19.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 20.56f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 22.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 23.56f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 25.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 26.56f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 28.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 29.56f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);

        // 第二段：31s - 55s，中等难度，部分 1 秒连续障碍
        AddBeat(id++, 31.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 32.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 33.56f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 35.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 36.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 37.56f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 39.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 40.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 41.56f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 43.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 44.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 45.56f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 47.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 48.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 49.56f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 51.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 52.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 53.56f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 55.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);

        // 第三段：57s - 80s，稍微加强，但比 70 障碍版轻
        AddBeat(id++, 57.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 58.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 59.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 60.56f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 62.06f, ObstacleType.Wall, ObstaclePattern.SingleGap, 6.5f);
        AddBeat(id++, 63.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 64.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 65.56f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 67.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 68.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 69.06f, ObstacleType.Wall, ObstaclePattern.SingleGap, 6.5f);
        AddBeat(id++, 70.56f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 72.06f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);
        AddBeat(id++, 73.06f, ObstacleType.Jump, ObstaclePattern.Full, 6.5f);
        AddBeat(id++, 74.06f, ObstacleType.Wall, ObstaclePattern.DoubleGap, 6.5f);
        AddBeat(id++, 75.56f, ObstacleType.Wall, ObstaclePattern.SingleGap, 6.5f);
        AddBeat(id++, 77.06f, ObstacleType.Wall, ObstaclePattern.LeftGap, 6.5f);
        AddBeat(id++, 78.06f, ObstacleType.Wall, ObstaclePattern.MiddleGap, 6.5f);
        AddBeat(id++, 79.56f, ObstacleType.Wall, ObstaclePattern.RightGap, 6.5f);

        ResetBeatMap();

        Debug.Log("Rhythm BeatMap built. Total events: " + beatEvents.Count);
    }

    private void AddBeat(int id, float targetTime, ObstacleType type, ObstaclePattern pattern, float speed = 6.5f)
    {
        float spawnTime = Mathf.Max(0f, targetTime - obstacleLeadTime);

        beatEvents.Add(new BeatEvent
        {
            id = id,
            time = spawnTime,
            type = type,
            pattern = pattern,
            speed = speed,
            spawned = false
        });
    }

    public List<BeatEvent> GetBeatEvents()
    {
        return beatEvents;
    }

    public void ResetBeatMap()
    {
        foreach (BeatEvent beatEvent in beatEvents)
        {
            beatEvent.spawned = false;
        }
    }

    public int GetTotalObstacleCount()
    {
        return beatEvents.Count;
    }
}