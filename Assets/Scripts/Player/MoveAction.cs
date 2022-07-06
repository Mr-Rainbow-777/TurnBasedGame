using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoveAction : BaseAction
{
    [Header("Variables")]
    [SerializeField]const float stopDistance = .1f;
    [SerializeField]private float MoveSpeed = 3f;
    [SerializeField]private float RotateSpeed = 10f;
    [SerializeField] private int MaxDistance=4;

    
    private Animator _animator;
    public Vector3 targetPosition;

    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    protected override void Start()
    {
        base.Start();
        targetPosition = this.transform.position;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            transform.position += moveDirection * MoveSpeed * Time.deltaTime;
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
            OnStopMoving?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * RotateSpeed);
    }

    public override void TakeAction(GridPosition targetpostion,Action complete)
    {
        ActionStart(complete);
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetpostion);
        OnStartMoving?.Invoke(this,EventArgs.Empty);
    }

    public override List<GridPosition> GetValidActionGridPostionList()
    {
        List<GridPosition> validGridPostionList = new List<GridPosition>();
        for (int x = -MaxDistance; x <= MaxDistance; x++)
        {
            for (int z = -MaxDistance; z <= MaxDistance; z++)
            {
                GridPosition offsetGridPostion = new GridPosition(x,z);
                GridPosition validPostion = unit.GetGridPosition() + offsetGridPostion;
                if(LevelGrid.Instance.IsValidGridPostion(validPostion) //未超出指定棋盘
                    &&unit.GetGridPosition()!=validPostion  //不是自身
                    &&!LevelGrid.Instance.HasAnyUnitOnGridPostion(validPostion))  //对应棋盘上没有Unit
                {
                    validGridPostionList.Add(validPostion);
                }


            }
        }
        return validGridPostionList;
    }


    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPostion)
    {
        int targetCountAtGridPos=unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPostion);
        return new EnemyAIAction
        {
            gridPosition = gridPostion,
            actionValue = targetCountAtGridPos *10,
        };
    }
}
