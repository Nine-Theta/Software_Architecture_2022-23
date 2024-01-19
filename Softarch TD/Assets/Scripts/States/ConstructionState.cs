using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[CreateAssetMenu(fileName = "ConstructionState", menuName = "States/ConstructionState")]
public class ConstructionState : AbstractProcessorState
{
    [SerializeField]
    private LayerMask _constructionLayer;


    public ConstructionState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        context.GetGameplayManager.ConstructionCommander.ExecuteCommand(new ConstructAtMouseRayCommand(context.GetGameplayManager, pMousePos, _constructionLayer));
    }
}
