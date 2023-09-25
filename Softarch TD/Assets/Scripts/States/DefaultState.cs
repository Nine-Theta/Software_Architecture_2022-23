using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultState", menuName = "States/DefaultState")]
public class DefaultState : AbstractProcessorState
{
    public DefaultState(InputProcessor pContext) : base(pContext)
    {

    }

    public override void ProccessButtonClick(Vector2 pMousePos)
    {
        throw new System.NotImplementedException();
    }
}
