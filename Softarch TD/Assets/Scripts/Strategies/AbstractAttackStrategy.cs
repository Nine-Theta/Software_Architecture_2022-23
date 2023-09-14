using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttackStrategy : ScriptableObject
{
    public abstract Collider GetTarget(Collider[] pTargets, Vector3 pCenter);
}
