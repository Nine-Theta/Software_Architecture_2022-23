using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProcessorState : ScriptableObject
{
    public InputProcessor context;

    public AbstractProcessorState (InputProcessor pContext)
    {
        this.context = pContext;
    }

    public abstract void ProccessButtonClick(Vector2 pMousePos);

    public virtual void SetContext(InputProcessor pContext)
    {
        context = pContext;
    }
}
