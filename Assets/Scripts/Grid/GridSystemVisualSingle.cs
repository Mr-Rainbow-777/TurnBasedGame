using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRender;

    private void Start()
    {
        _meshRender = GetComponentInChildren<MeshRenderer>();
    }

    public void Show(Material material)
    {
        _meshRender.enabled = true;
        _meshRender.material = material;
    }

    public void Hidden()
    {
        _meshRender.enabled = false;
    }
}
