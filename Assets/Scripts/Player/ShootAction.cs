using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    [SerializeField] private LayerMask ObastaclesLayerMask;

    public class OnShootEventArgs:EventArgs
    {
        public Unit targetUnit;
        public Unit ShootingUnit;
    }

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    private float totalSpinAmount;
    private int maxShootDistance=7;
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private bool canShootBullet;
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Aiming:
                    Vector3 aimDir = (targetUnit.GetWorldPos() - unit.GetWorldPos()).normalized;
                    float RotateSpeed = 10;
                    transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * RotateSpeed);
                break;
            case State.Shooting:
                if(canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
            default:
                break;
        }
        if(stateTimer<0)
        {
            NextState();
        }
    }


    private void Shoot()
    {
        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = targetUnit,
            ShootingUnit = unit,
        }); ; 
        targetUnit.Damage(40);
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                    state = State.Shooting;
                    float ShooringStateTime = 0.1f;
                    stateTimer = ShooringStateTime;
                break;
            case State.Shooting:
                    state = State.Cooloff;
                    float CoolOffStateTime = 0.5f;
                    stateTimer = CoolOffStateTime;               
                break;
            case State.Cooloff:
                ActionComplete();
                break;
            default:
                break;
        }
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPostionList()
    {
        GridPosition unitPosition = unit.GetGridPosition();
        return GetValidActionGridPostionList(unitPosition);
    }

    public List<GridPosition> GetValidActionGridPostionList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPostionList = new List<GridPosition>();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPostion = new GridPosition(x, z);
                GridPosition validPostion = unit.GetGridPosition() + offsetGridPostion;

                int testtarget=Mathf.Abs(x)+Mathf.Abs(z);
                if(testtarget>maxShootDistance)
                {
                    continue;
                }
                if (LevelGrid.Instance.IsValidGridPostion(validPostion) //未超出指定棋盘
                    && unit.GetGridPosition() != validPostion  //不是自身
                    && LevelGrid.Instance.HasAnyUnitOnGridPostion(validPostion))  //对应棋盘上有Unit
                {
                    Unit targetunit = LevelGrid.Instance.GetUnitOnGridPostion(validPostion);
                    if(targetunit.JudgeIsEnemy() == unit.JudgeIsEnemy())
                    {
                        continue;
                    }

                    float unitShoulderHeight = 1.7f;
                    Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                    Vector3 shootDir = (targetunit.GetWorldPos() - unitWorldPosition).normalized;

                    if(Physics.Raycast(unitWorldPosition + Vector3.up * unitShoulderHeight,
                        shootDir,
                        Vector3.Distance(unitWorldPosition, targetunit.GetWorldPos()),
                        ObastaclesLayerMask))
                    {
                        continue;
                    }
                    validGridPostionList.Add(validPostion);
                }
            }
        }
        return validGridPostionList;
    }

    public override void TakeAction(GridPosition gridposition, Action action)
    {

        targetUnit= LevelGrid.Instance.GetUnitOnGridPostion(gridposition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;
        ActionStart(action);
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }


    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPostion)
    {
        Unit targetUnit= LevelGrid.Instance.GetUnitOnGridPostion(gridPostion);

        return new EnemyAIAction
        {
            gridPosition = gridPostion,
            actionValue = 100 + Mathf.RoundToInt((1-targetUnit.GetHealthNormalized())*100f),
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPos)
    {
         return GetValidActionGridPostionList(gridPos).Count;

    }
}
