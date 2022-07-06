using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelGrid : MonoSingleton<LevelGrid>
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem<GridObject> gridSystem;

    public event EventHandler OnAnyUnitMovedGridPosition;

    [SerializeField] private int width;
    [SerializeField]private int Height;
    [SerializeField] private float cellSize;


    private void Awake()
    {
        gridSystem = new GridSystem<GridObject>(10, 10, 2f,(GridSystem<GridObject> g,GridPosition gridPosition)=>
        { 
            return new GridObject(g, gridPosition);
        });
        gridSystem.CreateGridObject(gridDebugObjectPrefab);
    }

    private void Start()
    {
        PathFinding.Instance.SetUp(width, Height, cellSize);
    }
    public void AddUnitAtGridPostion(GridPosition gridPosition,Unit unit)
    {
        GridObject gridobject=gridSystem.GetGridObject(gridPosition);
        gridobject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPostion(GridPosition gridpos)
    {
        GridObject gridobject = gridSystem.GetGridObject(gridpos);
        return gridobject.GetUnitList();
    }

    public void ClearUnitAtGridPostion(GridPosition gridpos,Unit unit)
    {
        GridObject gridobject = gridSystem.GetGridObject(gridpos);
        gridobject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit,GridPosition fromGridPostion,GridPosition ToGridPosition)
    {
        ClearUnitAtGridPostion(fromGridPostion,unit);
        AddUnitAtGridPostion(ToGridPosition,unit);

        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }


    public GridPosition GetGridPosition(Vector3 worldpos)=>gridSystem.GetGridPos(worldpos);
    public Vector3 GetWorldPosition(GridPosition gridPostion) => gridSystem.GetWorldPos(gridPostion);

    public bool IsValidGridPostion(GridPosition gridpositon) => gridSystem.IsValidGridPostion(gridpositon);
    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();
    public bool HasAnyUnitOnGridPostion(GridPosition gridPostion)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPostion);
        return gridObject.HasAnyUnit();
    }

    public Unit GetUnitOnGridPostion(GridPosition gridPostion)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPostion);
        return gridObject.GetUnit();
    }
}
