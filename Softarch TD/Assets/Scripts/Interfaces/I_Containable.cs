using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface that specifies the methods needed for a script to be used as instantion data by an implementation of <see cref="AbstractInstanceFactory"/>.
/// </summary>
/// <remarks>Used in conjunction with <see cref="AbstractContainerObject"/></remarks>
public interface I_Containable
{
    public string GetName { get; }
    public GameObject GetModel { get; }
    public int CreationCost { get; }
}
