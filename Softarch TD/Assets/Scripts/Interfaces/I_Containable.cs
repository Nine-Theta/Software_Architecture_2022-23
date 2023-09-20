using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Containable
{
    public string GetName { get; }
    public GameObject GetContainerObject { get; }
}
