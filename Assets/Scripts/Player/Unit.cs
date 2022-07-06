using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;

    private GridPosition _gridPosition;

    private BaseAction[] BaseActionArray;
    [SerializeField] private int actionPoints = ACTION_POINTS_MAX;
    [SerializeField] private bool IsEnemy;

    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;



    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem= GetComponent<HealthSystem>();
        BaseActionArray = GetComponents<BaseAction>();
    }
    void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPostion(_gridPosition, this);
        _healthSystem.OnDead += _healthSystem_OnDead;
        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }



    void Update()
    {
        GridPosition newGridpos = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridpos!=_gridPosition)
        {
            GridPosition oldpos = _gridPosition;
            _gridPosition =newGridpos;

            LevelGrid.Instance.UnitMovedGridPosition(this, oldpos, newGridpos);
        }
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
        if((JudgeIsEnemy()&&!TurnSystem.Instance.isPlayerTurn())||
            (!JudgeIsEnemy()&&TurnSystem.Instance.isPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }

    }


    public bool JudgeIsEnemy()
    {
        return this.IsEnemy;
    }

    internal void Damage(int damageAmount)
    {
        _healthSystem.Damage(damageAmount);
    }

    public Vector3 GetWorldPos()
    {
        return this.transform.position;
    }

    private void _healthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.ClearUnitAtGridPostion(_gridPosition, this);
        // Destroy(gameObject);
        gameObject.SetActive(false);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }


    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction baseAction in BaseActionArray)
        {
            if (baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }


    public float GetHealthNormalized()
    {
        return _healthSystem.GetHealthNormal();
    }



}
