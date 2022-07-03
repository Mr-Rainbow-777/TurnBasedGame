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
            this.ActionComplete?.Invoke();
            IsActive = false;
        }
    }

    public override void TakeAction(GridPosition gridPositon, Action spinfinish)
    {
        this.ActionComplete = spinfinish;
        IsActive = true;
        totalSpinAmount = 0;
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
}
