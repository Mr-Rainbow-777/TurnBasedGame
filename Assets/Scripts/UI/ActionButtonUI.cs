using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject SelectedGameObject;

    private BaseAction baseAction;
    private void Awake()
    {
        button=GetComponent<Button>();
        textMeshPro=GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction=baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() =>
        {
            SelectActionSystem.Instance.SetSelectedAction(baseAction);
        }

        );
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedAction = SelectActionSystem.Instance.GetSelectedAction();
        SelectedGameObject.SetActive(selectedAction == baseAction);
    }

}
