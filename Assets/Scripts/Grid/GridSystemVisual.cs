using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridSystemVisual : MonoSingleton<GridSystemVisual>
{
    [SerializeField] private Transform GridVisualPrefab;
    [SerializeField] private Transform root;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualMaterialList;


    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        WHITE,
        BLUE,
        RED,
        YELLOW,
    }



    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
            ];
        root = transform;
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridposition = new GridPosition(x, z);
                Transform gridSystemSingleTrans=Instantiate(GridVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridposition), Quaternion.identity,root);
                gridSystemVisualSingleArray[x, z] = gridSystemSingleTrans.GetComponent<GridSystemVisualSingle>();
            }
        }

        SelectActionSystem.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += Instance_OnAnyUnitMovedGridPosition;
    }



    public void HideAllGridPostion()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hidden();
            }
        }
    }
    private void Update()
    {
        UpdateGridVisual();
    }

    public void ShowGridPostionList(List<GridPosition> gridpostionList,GridVisualType gridtype)
    {
        foreach (var gridpostion in gridpostionList)
        {
            gridSystemVisualSingleArray[gridpostion.x, gridpostion.z].
                Show(GetGridVisualTypeMaterial(gridtype));
        }
    }

    public void UpdateGridVisual()
    {
        HideAllGridPostion();

        BaseAction selectedAction = SelectActionSystem.Instance.GetSelectedAction();
        GridVisualType gridtype = GridVisualType.WHITE;
        switch (selectedAction)
        {
            case MoveAction moveAction:
                gridtype = GridVisualType.WHITE;
                break;
            case SpinAction spinAction:
                gridtype = GridVisualType.BLUE;
                break;
            case ShootAction shootAction:
                gridtype = GridVisualType.RED;
                break;
            default:
                break;
        }
        ShowGridPostionList(selectedAction.GetValidActionGridPostionList(), gridtype);
    }


    private void Instance_OnSelectedActionChanged(object sender, System.EventArgs e)
    {
        UpdateGridVisual();
    }

    private void Instance_OnAnyUnitMovedGridPosition(object sender, System.EventArgs e)
    {
        UpdateGridVisual();

    }


    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualMaterialList)
        {
            if(gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }
        return null;
    }
}
