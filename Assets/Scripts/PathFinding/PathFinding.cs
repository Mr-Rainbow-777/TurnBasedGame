using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoSingleton<PathFinding>
{

    private const int MOVE_STRAIGT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private LayerMask obstaclesLayerMask;
    private int width;
    private int Height;
    private float cellSize;
    private GridSystem<PathNode> gridSystem;

    private void Awake()
    {

    }
  

    public List<GridPosition> FindPath(GridPosition startGridPosition,GridPosition endGridPosition,out int pathLength)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closeList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);

        openList.Add(startNode);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.setGCost(int.MaxValue);
                pathNode.setHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        startNode.setGCost(0);
        startNode.setHCost(CalculateDistance(startGridPosition,endGridPosition));
        startNode.CalculateFCost();

        while (openList.Count>0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if(currentNode==endNode)
            {
                //到达终点
                pathLength = endNode.GetFCost();
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach (PathNode pathNode in GetNeighbourList(currentNode))
            {
                if(closeList.Contains(pathNode))
                {
                    continue;
                }

                if(!pathNode.IsWalkAble())
                {
                    closeList.Add(pathNode);
                    continue;
                }


                int tentativeGCost=
         currentNode.GetGCost()+CalculateDistance(currentNode.GetGridPosition(),pathNode.GetGridPosition());


                if(tentativeGCost<pathNode.GetGCost())
                {
                    pathNode.SetCameFromPathNode(currentNode);
                    pathNode.setGCost(tentativeGCost);
                    pathNode.setHCost(CalculateDistance(pathNode.GetGridPosition(),endGridPosition));
                    pathNode.CalculateFCost();

                    if(!openList.Contains(pathNode))
                    {
                        openList.Add(pathNode);
                    }
                }
            }
        }

        //no path
        pathLength = 0;
        return null;
    }


    public int CalculateDistance(GridPosition gridPosA,GridPosition gridPosB)
    {
        GridPosition gridpositionDistance = gridPosA - gridPosB;
        int distance = Mathf.Abs(gridpositionDistance.x) + Mathf.Abs(gridpositionDistance.z);
        int xDistance = Mathf.Abs(gridpositionDistance.x);
        int zDistance = Mathf.Abs(gridpositionDistance.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return MOVE_DIAGONAL_COST*Mathf.Min(xDistance,zDistance)+MOVE_STRAIGT_COST*remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodelist)
    {
        PathNode lowestFCostPathNode = pathNodelist[0];
        for (int i =  1; i < pathNodelist.Count; i++)
        {
            if (pathNodelist[i].GetFCost()<lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode=pathNodelist[i];
            }
        }
        return lowestFCostPathNode;
    }


    public PathNode GetNode(int x,int z)
    {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }


    public List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        GridPosition gridpos = currentNode.GetGridPosition();

        if (gridpos.x - 1 >= 0)
        {
            //left
            neighbourList.Add(GetNode(gridpos.x - 1, gridpos.z));

            if (gridpos.z - 1 >= 0)
            {  //left down 
                neighbourList.Add(GetNode(gridpos.x - 1, gridpos.z - 1)); 
            }
            if (gridpos.z + 1  < gridSystem.GetHeight())
            {  //left up
                neighbourList.Add(GetNode(gridpos.x - 1, gridpos.z + 1));
            }
        }
        if (gridpos.x + 1 < gridSystem.GetWidth())
        {
            //right
            neighbourList.Add(GetNode(gridpos.x + 1, gridpos.z));
            if (gridpos.z - 1 >= 0)
            {
                //right down
                neighbourList.Add(GetNode(gridpos.x + 1, gridpos.z - 1));
            }
            if (gridpos.z + 1 < gridSystem.GetHeight())
            {
                //right up
                neighbourList.Add(GetNode(gridpos.x + 1, gridpos.z + 1));
            }
        }

        //up
        if (gridpos.z + 1 < gridSystem.GetHeight())
        {
            neighbourList.Add(GetNode(gridpos.x + 0, gridpos.z + 1));
        }
        if (gridpos.z - 1 >= 0)
        {
            //down
            neighbourList.Add(GetNode(gridpos.x + 0, gridpos.z - 1));
        }

        return neighbourList;
    }

    /// <summary>
    /// 输出整条路经
    /// </summary>
    /// <param name="endNode"></param>
    /// <returns></returns>
    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodelist = new List<PathNode>();
        pathNodelist.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.GetCameFromPathNode()!=null)
        {
            pathNodelist.Add(currentNode.GetCameFromPathNode());
            currentNode=currentNode.GetCameFromPathNode();
        }
         pathNodelist.Reverse();

        List<GridPosition> gridPos = new List<GridPosition>();
        foreach (PathNode pathNode in pathNodelist)
        {
            gridPos.Add(pathNode.GetGridPosition());
        }
        return gridPos;
    }


    public void SetUp(int width,int height,float cellsize)
    {
        this.width = width;
        this.Height = height;
        this.cellSize= cellsize;

        gridSystem = new GridSystem<PathNode>(10, 10, 2f, (GridSystem<PathNode> g, GridPosition gridPosition) =>
        {
            return new PathNode(gridPosition);
        });
        //gridSystem.CreateGridObject(gridDebugObjectPrefab);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPos=LevelGrid.Instance.GetWorldPosition(gridPosition);
                float raycastoffsetDistance = 5;
                if(Physics.Raycast(worldPos + Vector3.down * raycastoffsetDistance, 
                    Vector3.up, 
                    raycastoffsetDistance * 2, 
                    obstaclesLayerMask))
                {
                    GetNode(x, z).SetIsWalkAble(false);
                }
                
            }
        }

    }
    public bool IsWalkAble(GridPosition gridPos)
    {
        return gridSystem.GetGridObject(gridPos).IsWalkAble();
    }
     
    public bool HasPath(GridPosition startPosition,GridPosition endPosition)
    {
        return PathFinding.Instance.FindPath(startPosition,endPosition,out int pathLength) != null;
    }


    public int GetPathLength(GridPosition startPosition,GridPosition endPosition)
    {
        FindPath(startPosition, endPosition, out int pathLength);
        return pathLength;
    }
}
