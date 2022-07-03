using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool IsActive;
    protected Action ActionComplete;
    protected virtual void Start()
    { 
        unit= GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridposition, Action action);

    public virtual bool IsValidActionGridPostion(GridPosition gridPostion)
    {
        List<GridPosition> validGridPsotion = GetValidActionGridPostionList();
        return validGridPsotion.Contains(gridPostion);
    }

    public abstract List<GridPosition> GetValidActionGridPostionList();

    public virtual int GetActionPointCost()
    {
        return 1;
    }
}
