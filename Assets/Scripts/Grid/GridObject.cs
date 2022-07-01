using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;
    private PlayerControl unit;
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", _gridPosition.x, _gridPosition.z);
    }


    public void SetUnit(PlayerControl unit)
    {
        this.unit = unit;
    }

    public PlayerControl GetUnit()
    {
        return this.unit;
    }
}
