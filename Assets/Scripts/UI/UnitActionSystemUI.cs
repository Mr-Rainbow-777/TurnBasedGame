using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform ActionClickButton;
    [SerializeField] private Transform root;
    [SerializeField] private TextMeshProUGUI PointersText;
    private List<ActionButtonUI> actionButtonList;


    private void Start()
    {
        actionButtonList = new List<ActionButtonUI>();
        SelectActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
        SelectActionSystem.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
        SelectActionSystem.Instance.OnActionStart += Instance_OnActionStart;
        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointers();
    }

    private void Instance_OnTurnChnage(object sender, EventArgs e)
    {
        UpdateActionPointers();
    }

    private void Instance_OnActionStart(object sender, EventArgs e)
    {
        UpdateActionPointers();
    }

    private void Instance_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void Instance_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointers();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform item in root)
        {
            Destroy(item.gameObject);
        }
        actionButtonList.Clear();
        Unit unit = SelectActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction action in unit.GetBaseActionArray())
        {
            Transform actionbutton= Instantiate(ActionClickButton, root);
            ActionButtonUI actionButtonUI=actionbutton.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(action);
            actionButtonList.Add(actionButtonUI);
        }
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPointers()
    {
        Unit unit=SelectActionSystem.Instance.GetSelectedUnit();
        PointersText.text = "Leave Action Points:" + unit.GetCurrentPoints();
    }
}
