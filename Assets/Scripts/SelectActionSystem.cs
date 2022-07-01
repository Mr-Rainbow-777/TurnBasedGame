using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActionSystem : MonoSingleton<SelectActionSystem>
{
    [SerializeField] private PlayerControl Selectedunit;
    [SerializeField] private LayerMask UnitLayerMask;


    public event EventHandler OnSelectedUnitChanged;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleSelectUnit()) { return; }
            Selectedunit.Move(MouseWorld.Instance.GetDistance());
        }

    }

    private bool HandleSelectUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, int.MaxValue, UnitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<PlayerControl>(out PlayerControl playerControl))
            {
                SetSelectedUnit(playerControl);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(PlayerControl unit)
    {
        Selectedunit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public PlayerControl GetSelectedUnit()
    {
        return Selectedunit;
    }
    
}
