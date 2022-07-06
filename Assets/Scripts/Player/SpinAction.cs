using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    protected override void Start()
    {
        base.Start();

    }
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;
        if(totalSpinAmount>=360f)
        {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPositon, Action spinfinish)
    {
        totalSpinAmount = 0;
        ActionStart(spinfinish);
    }

    public override string GetActionName()
    {
        return "Spin";
    }



    public override List<GridPosition> GetValidActionGridPostionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointCost()
    {
        return 2;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPostion)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPostion,
            actionValue = 0,
        };
    }
}
