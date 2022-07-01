using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoSingleton<LevelGrid>
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    private void Awake()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateGridObject(gridDebugObjectPrefab);
    }

    public void SetUnitAtGridPostion(GridPosition gridPosition,PlayerControl unit)
    {
        GridObject gridobject=gridSystem.GetGridObject(gridPosition);
        gridobject.SetUnit(unit);
    }

    public PlayerControl GetUnitAtGridPostion(GridPosition gridpos)
    {
        GridObject gridobject = gridSystem.GetGridObject(gridpos);
        return gridobject.GetUnit();
    }

    public void ClearUnitAtGridPostion(GridPosition gridpos)
    {
        GridObject gridobject = gridSystem.GetGridObject(gridpos);
        gridobject.SetUnit(null);
    }

    public void UnitMovedGridPosition(PlayerControl unit,GridPosition fromGridPostion,GridPosition ToGridPosition)
    {
        ClearUnitAtGridPostion(fromGridPostion);
        SetUnitAtGridPostion(ToGridPosition,unit);
    }


    public GridPosition GetGridPosition(Vector3 worldpos)=>gridSystem.GetGridPos(worldpos);
}
