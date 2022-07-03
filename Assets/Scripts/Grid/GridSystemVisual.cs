using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoSingleton<GridSystemVisual>
{
    [SerializeField] private Transform GridVisualPrefab;
    [SerializeField] private Transform root;

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
    }

    public void HiadAllGridPostion()
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

    public void ShowGridPostionList(List<GridPosition> gridpostionList)
    {
        foreach (var gridpostion in gridpostionList)
        {
            gridSystemVisualSingleArray[gridpostion.x, gridpostion.z].Show();
        }
    }

    public void UpdateGridVisual()
    {
        HiadAllGridPostion();

        BaseAction selectedAction = SelectActionSystem.Instance.GetSelectedAction();

        ShowGridPostionList(selectedAction.GetValidActionGridPostionList());
    }

}
