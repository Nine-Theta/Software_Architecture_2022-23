using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovementStrategy : ScriptableObject
{
    public abstract void MoveStep(Vector3 pDestiniation);
}
