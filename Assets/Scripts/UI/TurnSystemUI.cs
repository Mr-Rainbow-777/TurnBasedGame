using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endButton;
    [SerializeField] private TextMeshProUGUI turnNumber;


    private void Start()
    {
        endButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;
        UpdateTurnNext();
    }

    private void Instance_OnTurnChnage(object sender, System.EventArgs e)
    {
        UpdateTurnNext();
    }

    private void UpdateTurnNext()
    {
        turnNumber.text = "TURN" + TurnSystem.Instance.GetTurnNumber();
    }
}
