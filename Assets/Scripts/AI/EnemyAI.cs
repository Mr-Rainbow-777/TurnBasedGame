using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,

    }

    private State state;
    private float timer;

    private void Awake()
    {
        state = State.WaitingForEnemyTurn;
    }

    private void Start()
    {
        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;
    }



    private void Update()
    {
        if (TurnSystem.Instance.isPlayerTurn())
        {
            return;
        }
        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if(timer<0)
                {
                    if(TryTakeEnemyAIAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
            default:
                break;
        }
        timer-= Time.deltaTime;
        if(timer < 0)
        {
            TurnSystem.Instance.NextTurn();
        }
    }

    private void SetStateTakingTurn()
    {
        timer = .5f;
        state = State.TakingTurn;
    }

    private void Instance_OnTurnChnage(object sender, System.EventArgs e)
    {
        if(!TurnSystem.Instance.isPlayerTurn())
        {
            state = State.TakingTurn;
            timer = 2f;
        }

    }

    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete);
            return true;
        }
        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;

       foreach(BaseAction baseAction in  enemyUnit.GetBaseActionArray())
       {
            if(!enemyUnit.CanSpendActionPointsToTakeAction(baseAction))
            {
                continue;
            }
            if(bestEnemyAIAction==null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction();
                if(testEnemyAIAction!=null&&testEnemyAIAction.actionValue> bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = testEnemyAIAction;
                    bestBaseAction = baseAction;
                }
            }            
       }

       if(bestEnemyAIAction!=null&&enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition,onEnemyAIActionComplete);
            return true;
        }
       else
        {
            return false;
        }
        
    }
}
