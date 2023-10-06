using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ConstructionState", menuName = "States/ConstructionState")]
public class ConstructionState : AbstractProcessorState
{
    [SerializeField]
    private LayerMask _buildingLayer;

    public ConstructionState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        if (context.Credits - context._selectedTower.Cost < 0 || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 50, context.ConstructionFactory.GetBuildLayer());
        if (hit.collider == null) return;

        BuildTower(hit.point);

        context.Credits -= context._selectedTower.Cost;

        //if (hit.collider.tag)
        //TowerBuildPosition = hit.point;
        //TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
    }

    public void ChangeTargetLayer(LayerMask pTargetLayer)
    {
        _buildingLayer = pTargetLayer;
    }

    private void BuildTower(Vector3 pPosition)
    {
        context.ConstructionCommander.ExecuteCommand(new ConstructionCommand(context.ConstructionFactory, pPosition));
    }
}
