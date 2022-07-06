using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurnSystem : MonoSingleton<TurnSystem>
{
    private int turnNumber=0;
    public event EventHandler OnTurnChnage;

    private bool IsPlayerTurn=true;

    public void NextTurn()
    {
        turnNumber++;
        IsPlayerTurn = !IsPlayerTurn;
        OnTurnChnage?.Invoke(this,EventArgs.Empty);
    }
    

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool isPlayerTurn()
    {
        return IsPlayerTurn;
    }
}
