using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : MonoBehaviour
{
    [Header("Wall Settings")]
    public GameObject wallBlockPrefab;
    public ObstaclePattern pattern = ObstaclePattern.MiddleGap;

    [Header("Grid Settings")]
    public int columnCount = 7;
    public float gridSize = 1.5f;
    public float blockHeight = 2.5f;
    public float blockDepth = 0.5f;

    private readonly List<GameObject> spawnedBlocks = new List<GameObject>();

    private void Start()
    {
        BuildWall();
    }

    public void SetPattern(ObstaclePattern newPattern)
    {
        pattern = newPattern;
        BuildWall();
    }

    public void BuildWall()
    {
        ClearOldBlocks();

        if (wallBlockPrefab == null)
        {
            Debug.LogWarning("WallObstacle 缺少 wallBlockPrefab");
            return;
        }

        bool[] blocked = GetBlockedColumns(pattern);

        for (int i = 0; i < columnCount; i++)
        {
            if (!blocked[i])
            {
                continue;
            }

            float x = GetColumnX(i);

            GameObject block = Instantiate(wallBlockPrefab, transform);
            block.name = "Block_" + i;

            block.transform.localPosition = new Vector3(x, blockHeight / 2f, 0f);
            block.transform.localRotation = Quaternion.identity;
            block.transform.localScale = new Vector3(gridSize * 0.9f, blockHeight, blockDepth);

            spawnedBlocks.Add(block);
        }
    }

    private bool[] GetBlockedColumns(ObstaclePattern obstaclePattern)
    {
        bool[] blocked = new bool[columnCount];

        for (int i = 0; i < columnCount; i++)
        {
            blocked[i] = true;
        }

        switch (obstaclePattern)
        {
            case ObstaclePattern.LeftGap:
                // 左侧留两个缺口
                blocked[0] = false;
                blocked[1] = false;
                break;

            case ObstaclePattern.MiddleGap:
                // 中间留两个缺口
                blocked[3] = false;
                blocked[4] = false;
                break;

            case ObstaclePattern.RightGap:
                // 右侧留两个缺口
                blocked[5] = false;
                blocked[6] = false;
                break;

            case ObstaclePattern.DoubleGap:
                // 左右各留一个缺口
                blocked[1] = false;
                blocked[5] = false;
                break;

            case ObstaclePattern.SingleGap:
                // 只留中间一个缺口
                blocked[3] = false;
                break;

            case ObstaclePattern.Full:
                // 全部堵住，一般不建议用于墙
                break;
        }

        return blocked;
    }

    private float GetColumnX(int index)
    {
        float centerIndex = (columnCount - 1) / 2f;
        return (index - centerIndex) * gridSize;
    }

    private void ClearOldBlocks()
    {
        for (int i = spawnedBlocks.Count - 1; i >= 0; i--)
        {
            if (spawnedBlocks[i] != null)
            {
                Destroy(spawnedBlocks[i]);
            }
        }

        spawnedBlocks.Clear();
    }
}
