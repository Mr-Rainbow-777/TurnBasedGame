using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    private List<Unit> unitlist;
    private List<Unit> friendUnitList;
    private List<Unit> enemyUnitList;



    private void Awake()
    {
        unitlist = new List<Unit>();
        friendUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
    }
    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead; 

    }

    private void Unit_OnAnyUnitDead(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;

        unitlist.Remove(unit);
        if(unit.JudgeIsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendUnitList.Remove(unit);
        }

    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;

        unitlist.Add(unit);
        if (unit.JudgeIsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendUnitList.Add(unit);
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitlist;
    }

    public List<Unit> GetFriendUnitList()
    {
        return friendUnitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
}
