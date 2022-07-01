using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{
    private int height;
    private int width;
    private float cellSize;   

    private GridObject[,] gridObjectArray;

    public GridSystem(int height, int width, float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition pos = new GridPosition(x, z);
                gridObjectArray[x,z]=new GridObject(this, pos);
            }
        }
    }


    public Vector3 GetWorldPos(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0, gridPos.z)*cellSize;
    }

    public GridPosition GetGridPos(Vector3 worldPos)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPos.x/cellSize),
            Mathf.RoundToInt(worldPos.z/cellSize))
            ;
    }


    public void CreateGridObject(Transform debugGridPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugGridPrefab, GetWorldPos(gridPos), Quaternion.identity);
                GridDebugObject gridObject= debugTransform.GetComponent<GridDebugObject>();
                gridObject.SetGridObject(GetGridObject(gridPos));
            }
        }
    }


    public GridObject GetGridObject(GridPosition gridpos)
    {
        return gridObjectArray[gridpos.x, gridpos.z];
    }
}
