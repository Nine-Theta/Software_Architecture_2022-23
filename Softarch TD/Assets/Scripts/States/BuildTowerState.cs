using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildState", menuName = "States/ConstructionState")]
public class BuildTowerState : AbstractProcessorState
{
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 1, 0);

    [SerializeField]
    private LayerMask _buildingLayer;

    public BuildTowerState(InputProcessor pContext) : base(pContext)
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

        Physics.Raycast(ray, out hit, 50, _buildingLayer);
        if (hit.collider == null) return;

        BuildTower(hit.point);

        context.Credits -= context._selectedTower.Cost;

        //if (hit.collider.tag)
        //TowerBuildPosition = hit.point;
        //TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
    }

    private void BuildTower(Vector3 pPosition)
    {
        context.TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(context.TowerFactory, context._selectedTower, pPosition+_offset));
    }
}
