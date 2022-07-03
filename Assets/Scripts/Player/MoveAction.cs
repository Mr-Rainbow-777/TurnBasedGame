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
            IsActive = false;
            this.ActionComplete?.Invoke();
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * RotateSpeed);
    }

    public override void TakeAction(GridPosition targetpostion,Action complete)
    {
        this.ActionComplete = complete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetpostion);
        IsActive = true;
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
                if(LevelGrid.Instance.IsValidGridPostion(validPostion) //δ����ָ������
                    &&unit.GetGridPosition()!=validPostion  //��������
                    &&!LevelGrid.Instance.HasAnyUnitOnGridPostion(validPostion))  //��Ӧ������û��Unit
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
}
