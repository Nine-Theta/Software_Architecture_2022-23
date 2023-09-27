using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildState", menuName = "States/ConstructionState")]
public class BuildTowerState : AbstractProcessorState
{
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 1, 0);

    public BuildTowerState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 50, 100001);
        if (hit.collider == null) return;

        BuildTower(hit.point);

        //if (hit.collider.tag)
        //TowerBuildPosition = hit.point;
        //TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
    }

    private void BuildTower(Vector3 pPosition)
    {
        context.TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(context.TowerFactory, context._selectedTower, pPosition+_offset));
    }
}
