using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private GridPosition gridPosition;
    private int gCost; //距离初始点的价值
    private int hCost; //距离目标点的价值
    private int fCost; //g+h
    private PathNode cameFromPathNode;
    private bool IsWalkable = true;
    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    public override string ToString()
    {
        return gridPosition.ToString();
    }

    internal int GetGCost()
    {
        return gCost;
    }

    internal int GetHCost()
    {
        return hCost;

    }

    internal int GetFCost()
    {
        return fCost;

    }

    internal void setGCost(int G)
    {
        this.gCost = G;
    }

    internal void setHCost(int H)
    {
        this.hCost = H;
    }
    internal void CalculateFCost()
    {
        this.fCost = gCost + hCost;
    }


    public void SetCameFromPathNode(PathNode pathnode)
    {
        cameFromPathNode = pathnode;
    }
    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    internal PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }

    public bool IsWalkAble()
    {
        return IsWalkable;
    }


    public void SetIsWalkAble(bool iswalkable)
    {
        this.IsWalkable = iswalkable;
    }
}
