using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TurnSystem : MonoSingleton<TurnSystem>
{
    private int turnNumber=0;
    public event EventHandler OnTurnChnage;


    public void NextTurn()
    {
        turnNumber++;

        OnTurnChnage?.Invoke(this,EventArgs.Empty);
    }
    

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
