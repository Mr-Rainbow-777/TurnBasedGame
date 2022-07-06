using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endButton;
    [SerializeField] private TextMeshProUGUI turnNumber;
    [SerializeField] private GameObject enemyTurnVisualGameObj;

    private void Start()
    {
        endButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChnage += Instance_OnTurnChnage;
        UpdateTurnNext();
        UpdateEnemyTurnVisual();
    }

    private void Instance_OnTurnChnage(object sender, System.EventArgs e)
    {
        UpdateTurnNext();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisual();
    }

    private void UpdateTurnNext()
    {
        turnNumber.text = "TURN" + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEnemyTurnVisual()
    {
        this.enemyTurnVisualGameObj.SetActive(!TurnSystem.Instance.isPlayerTurn());
    }

    private void UpdateEndTurnButtonVisual()
    {
        endButton.gameObject.SetActive(TurnSystem.Instance.isPlayerTurn());
    }
}
