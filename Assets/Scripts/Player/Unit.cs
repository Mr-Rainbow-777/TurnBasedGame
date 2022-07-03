using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;

    private GridPosition _gridPosition;
    private MoveAction _moveAction;
    private SpinAction _SpinAction;
    private BaseAction[] BaseActionArray;
    [SerializeField] private int actionPoints = ACTION_POINTS_MAX;


    public static event EventHandler OnAnyActionPointsChanged;


    private void Awake()
    {
        _moveAction = this.transform.GetComponent<MoveAction>();
        _SpinAction = this.transform.GetComponent<SpinAction>();
        BaseActionArray = GetComponents<BaseAction>();
    }
    void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPostion(_gridPosition, this);

        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;
    }



    void Update()
    {
        GridPosition newGridpos = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridpos!=_gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition, newGridpos);
            _gridPosition=newGridpos; 
        }
    }

    public MoveAction GetMoveAction()
    {
        return this._moveAction;
    }
    public SpinAction GetSpinAction()
    {
        return this._SpinAction;
    }
    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return this.BaseActionArray;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(actionPoints>=baseAction.GetActionPointCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointCost());
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SpendActionPoints(int amount)
    {
        actionPoints-=amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetCurrentPoints()
    {
        return actionPoints;
    }

    private void Instance_OnTurnChnage(object sender, System.EventArgs e)
    {
        actionPoints = ACTION_POINTS_MAX;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}
