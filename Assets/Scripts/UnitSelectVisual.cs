using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectVisual : MonoBehaviour
{
    [SerializeField] private PlayerControl unit;

    private MeshRenderer _meshrender;

    private void Awake()
    {
        _meshrender=GetComponent<MeshRenderer>();
    }


    private void Start()
    {
        SelectActionSystem.Instance.OnSelectedUnitChanged += OnSelectedVisual;

        UpdateVisual();
    }

    private void OnSelectedVisual(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        if (SelectActionSystem.Instance.GetSelectedUnit() == unit)  //��Ҫ��������ֱ���ñ�ѡ�е�Ŀ��  �����������Ӱ�ȫ�൱������
        {
            _meshrender.enabled = true;
        }
        else
        {
            _meshrender.enabled = false;
        }
    }
}
