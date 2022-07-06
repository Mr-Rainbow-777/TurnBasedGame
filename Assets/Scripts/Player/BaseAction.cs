using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{

    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;


    protected Unit unit;
    protected bool IsActive;
    protected Action OnActionComplete;
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

    protected void ActionStart(Action onActionComplete)
    {
        IsActive = true;
        this.OnActionComplete = onActionComplete;
        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        IsActive = false; 
        OnActionComplete();

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }


    public Unit GetUnit()
    {
        return unit;
    }

    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAIActionList = new List<EnemyAIAction>();
        List<GridPosition> validActionGridPosList = GetValidActionGridPostionList();

        foreach (GridPosition gridPosition in validActionGridPosList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(gridPosition);
            enemyAIActionList.Add(enemyAIAction);
        }
        if (enemyAIActionList.Count > 0)
        {
            enemyAIActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAIActionList[0];
        }
        else
        {
            return null;
        }
        

    }

    public abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPostion);
}
