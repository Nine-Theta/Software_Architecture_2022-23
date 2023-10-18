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
    private List<LayerMask> _constructionLayers;

    private LayerMask _buildingLayer;


    public ConstructionState(InputProcessor pContext) : base(pContext)
    {
       
    }

    public void OnEnable()
    {
        _buildingLayer = new LayerMask();

        for (int i = 0; i < _constructionLayers.Count; i++)
        {
            _buildingLayer += _constructionLayers[i];
        }
    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        if (context.Credits - context.ConstructionFactory.Containable.CreationCost < 0 || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(pMousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.green, 3);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 50, _buildingLayer);

        if (hit.collider == null || (context.ConstructionFactory.GetBuildLayer().value & (1 << hit.collider.gameObject.layer)) == 0 ) return;

        Vector3 buildCoords = hit.point;

        if (hit.collider.CompareTag("Foundation"))
        {
            FoundationObject FO = hit.collider.GetComponent<FoundationObject>();

            if (!FO.BuildRequest())
                return;

            buildCoords = FO.GetBuildPos;
        }

        ConstructAt(buildCoords);

        context.Credits -= context.ConstructionFactory.Containable.CreationCost;

        //if (hit.collider.tag)
        //TowerBuildPosition = hit.point;
        //TowerBuildCommander.ExecuteCommand(new BuildTowerCommand(this));
    }

    public void ChangeTargetLayer(LayerMask pTargetLayer)
    {
        _buildingLayer = pTargetLayer;
    }

    private void ConstructAt(Vector3 pPosition)
    {
        context.ConstructionCommander.ExecuteCommand(new ConstructionCommand(context.ConstructionFactory, pPosition));
    }
}
