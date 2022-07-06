using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PathFindingDebugObject :GridDebugObject
{
    [SerializeField] private TextMeshPro gCostText;
    [SerializeField] private TextMeshPro hCostText;
    [SerializeField] private TextMeshPro fCostText;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private PathNode pathNode;
    public override void SetGridObject(object gridobject)
    {
        base.SetGridObject(gridobject);
        pathNode = (PathNode)gridobject;
    }


    protected override void Update()
    {
        base.Update();
        gCostText.text = pathNode.GetGCost().ToString();
        hCostText.text = pathNode.GetHCost().ToString();
        fCostText.text = pathNode.GetFCost().ToString();
        spriteRenderer.color=pathNode.IsWalkAble()?Color.green: Color.red;
    }
}
