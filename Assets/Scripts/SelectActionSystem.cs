using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectActionSystem : MonoSingleton<SelectActionSystem>
{
    [SerializeField] private Unit Selectedunit;
    [SerializeField] private LayerMask UnitLayerMask;

    private BaseAction SelectAction;
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStart;

    private bool IsBusy;


    private void Start()
    {
        SetSelectedUnit(Selectedunit);
    }
    private void Update()
    {
        if (IsBusy)
        {
            return;
        }

        if(EventSystem.current.IsPointerOverGameObject()) //防止在按键时执行相关行为
        {
            return;
        }
        if (HandleSelectUnit()) 
        {
            return; 
        }       
        HandleSelectedAction();
    }




    private bool HandleSelectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, int.MaxValue, UnitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == Selectedunit)
                    {
                        //Unit 已经被选中了
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    
    }

    private void SetSelectedUnit(Unit unit)
    {
        Selectedunit = unit;
        SelectAction = unit.GetMoveAction();
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        SelectAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleSelectedAction()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GridPosition mousePsotion = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetPositon());

            if (!SelectAction.IsValidActionGridPostion(mousePsotion))
            {
                return;
            }
            if (!Selectedunit.TrySpendActionPointsToTakeAction(SelectAction))
            {
                return;
            }
            SetBusy();
            SelectAction.TakeAction(mousePsotion, ClearBusy);
            OnActionStart?.Invoke(this, EventArgs.Empty);
        }

        
    }



    public Unit GetSelectedUnit()
    {
        return Selectedunit;
    }
    
    public void SetBusy()
    {
        IsBusy = true;
        OnBusyChanged?.Invoke(this, IsBusy);
    }
    public void ClearBusy()
    {
        IsBusy = false;
        OnBusyChanged?.Invoke(this, IsBusy);
    }


    public BaseAction GetSelectedAction()
    {
        return SelectAction;
    }
}
