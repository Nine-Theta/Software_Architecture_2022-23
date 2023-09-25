using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildState", menuName = "States/ConstructionState")]
public class BuildTowerState : AbstractProcessorState
{


    public BuildTowerState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        Debug.Log("click");

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 50, 10001);
        if (hit.collider == null) return;

        Debug.Log("hit!");

        BuildTower(hit.point);

        //if (hit.collider.tag)
        //TowerBuildPosition = hit.point;
        //TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
    }

    private void BuildTower(Vector3 pPosition)
    {
        Debug.Log(context.name);
        Debug.Log(context.TowerBuilder);
        Debug.Log(context._selectedTower);
        Debug.Log(pPosition);
        context.TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(context.TowerBuilder, context._selectedTower, pPosition));
    }
}
