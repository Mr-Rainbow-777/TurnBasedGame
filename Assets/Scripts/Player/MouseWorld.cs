using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoSingleton<MouseWorld>
{

    private RaycastHit raycastHit;
    [SerializeField]
    private LayerMask MouseLayer;
    void Update()
    {
        this.transform.position = GetDistance();
    }

    public Vector3 GetDistance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out raycastHit, int.MaxValue, MouseLayer);
        return raycastHit.point;
    }
}
