using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Block> grid = new Dictionary<Vector2Int, Block>(); 

    [SerializeField] Block StartPoint, EndPoint; // Start and Finish Blocks

    Queue<Block> queue = new Queue<Block>(); // Queue of searching blocks

    bool isRunning = true; //  If the programm has not yet found a path - true

        Block SearchPoint; // Current SearchPoint

    public List<Block> path = new List<Block>(); // Actually the path

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Block> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            PathFind();
            CreatePath();
            return path;
        }
        return path;
    }

    private void CreatePath()
    {
        AddPointToPath(EndPoint);

        Block prevPoint = EndPoint.exploredFrom;
        while(prevPoint != StartPoint)
        {
            prevPoint.SetTopColor(Color.grey);
            AddPointToPath(prevPoint);
            prevPoint = prevPoint.exploredFrom;
        }
        AddPointToPath(StartPoint);
        path.Reverse();
    }

    private void AddPointToPath(Block point)
    {
        path.Add(point);
        point.isPlaceable = false;
    }
    private void PathFind()
    {
        queue.Enqueue(StartPoint);
        while(queue.Count > 0 && isRunning)
        { 
            SearchPoint = queue.Dequeue();
            SearchPoint.isExplored = true;
            CheckForEndPoint();
            ExploreNearPoints();
        }
    }
    private void CheckForEndPoint()
    {
        if (SearchPoint == EndPoint)
        {
            isRunning = false;
        }
    }
    private void ExploreNearPoints()
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direct in directions)
        {
            Vector2Int NearPointCords = SearchPoint.GetGridPosition() + direct;
            if (grid.ContainsKey(NearPointCords))
            {
                Block NearBlock = grid[NearPointCords];
                AddPointToQueue(NearBlock);
            } else
            {

            }
        }
    }
    private void AddPointToQueue(Block NearBlock)
    {
        if (NearBlock.isExplored || queue.Contains(NearBlock))
        {
            return;
        }
        else
        {
            queue.Enqueue(NearBlock);
            NearBlock.exploredFrom = SearchPoint;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Block>();
        foreach(Block block in waypoints)
        {
            Vector2Int gridPos = block.GetGridPosition();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Repeat: " + block);
            }
            else
            {
                grid.Add(gridPos, block);
            }
        }
        print("Was added "+grid.Count+" Blocks");
    }
}
