using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private object m_GridObject;

    [SerializeField] private TextMeshPro m_textmesh;
    public virtual void SetGridObject(object gridobject)
    {
        this.m_GridObject = gridobject;
    }

    protected virtual void Update()
    {
        //m_textmesh.text = m_GridObject.ToString();
    }


}
