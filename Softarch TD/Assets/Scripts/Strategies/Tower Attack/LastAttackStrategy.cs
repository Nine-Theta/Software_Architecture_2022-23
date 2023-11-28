using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LastAttackStrategy", menuName = "Strategy/Attack/Last")]
public class LastAttackStrategy : AbstractAttackStrategy
{
    public override Collider GetTarget(Collider[] pTargetsInRange, Vector3 pReference)
    {
        if (pTargetsInRange.Length == 0) return null;

        return pTargetsInRange[pTargetsInRange.Length-1];
    }

    public override bool TryGetTarget(Collider[] pTargetsInRange, Vector3 pReference, out Collider pTarget)
    {
        pTarget = GetTarget(pTargetsInRange, pReference);

        if (pTarget == null)
            return false;
        else
            return true;
    }
}

