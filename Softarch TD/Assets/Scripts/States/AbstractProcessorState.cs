using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that serves as the base for the states used by <see cref="InputProcessor"/>
/// </summary>
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
