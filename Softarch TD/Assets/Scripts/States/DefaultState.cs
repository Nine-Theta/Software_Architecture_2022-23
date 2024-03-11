using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Default state for the <see cref="InputProcessor"/>. It does not do anything and is not really used.
/// </summary>
[CreateAssetMenu(fileName = "DefaultState", menuName = "States/DefaultState")]
public class DefaultState : AbstractProcessorState
{
    public DefaultState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        //throw new System.NotImplementedException();
    }
}
