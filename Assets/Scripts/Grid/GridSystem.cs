using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private int height;
    private int width;
    private int cellSize;

    public GridSystem(int height, int width, int cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Debug.DrawLine(GetWorldPos(x, z), GetWorldPos(x, z) + Vector3.right * .2f, Color.white, 1000);
            }
        }
    }


    public Vector3 GetWorldPos(int x,int z)
    {
        return new Vector3(x, 0, z)*cellSize;
    }

    public GridPosition GetGridPos(Vector3 worldPos)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPos.x/cellSize),
            Mathf.RoundToInt(worldPos.z/cellSize))
            ;
    }
}
