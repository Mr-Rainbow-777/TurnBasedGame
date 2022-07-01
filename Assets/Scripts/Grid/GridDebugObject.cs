using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject m_GridObject;

    [SerializeField] private TextMeshPro m_textmesh;
    public void SetGridObject(GridObject gridobject)
    {
        this.m_GridObject = gridobject;
    }

    public void Update()
    {
        m_textmesh.text = m_GridObject.ToString();
    }


}
