using System;
using UnityEngine;

[Serializable]
public class BeatEvent
{
    [Header("Basic")]
    public int id;
    public float time;

    [Header("Obstacle")]
    public ObstacleType type;
    public ObstaclePattern pattern;

    [Header("Move")]
    public float speed = 5f;

    [Header("Runtime")]
    public bool spawned = false;
}