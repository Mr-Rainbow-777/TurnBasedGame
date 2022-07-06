using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem<GridObject> _gridSystem;
    private GridPosition _gridPosition;
    private List<Unit> unitlist;
    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
        unitlist = new List<Unit>();
    }

    public override string ToString()
    {
        string unitstring = "";
        foreach (Unit unit in unitlist)
        {
            unitstring += unit + "\n";
        }
        return string.Format("x:{0} y:{1}\n{2}", _gridPosition.x, _gridPosition.z,unitstring);
    }


    public void AddUnit(Unit unit)
    {
        unitlist.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitlist.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return this.unitlist;
    }

    public bool HasAnyUnit()
    {
        return unitlist.Count > 0;
    }

    public Unit GetUnit()
    {
        if(HasAnyUnit())
        {
            return unitlist[0];
        }else
        {
            return null;
        }
    }
}
