using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

/// <summary>
/// Construction state for the <see cref="InputProcessor"/>.
/// In this state the processor will attempt to build constructable items using the <see cref="ConstructAtMouseRayCommand"/> on MouseClick.
/// </summary>
/// <remarks>The constructable items are <see cref="FoundationObject"/>s and <see cref="TowerObject"/>s</remarks>
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
        context.GetGameplayManager.ConstructionCommander.SetCommand(new ConstructAtMouseRayCommand(context.GetGameplayManager, pMousePos, _constructionLayer));

        context.GetGameplayManager.ConstructionCommander.ExecuteCommand();
    }
}
