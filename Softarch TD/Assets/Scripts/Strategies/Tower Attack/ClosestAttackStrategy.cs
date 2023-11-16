using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ClosestAttackStrategy", menuName = "Strategy/Attack/Closest")]
public class ClosestAttackStrategy : AbstractAttackStrategy
{
    /// <param name="pTargetsInRange">A collider array of all targets to check</param>
    /// <param name="pCenterPosition">The tower's position to check the collider locations against</param>
    /// <returns>The closest collider to the Center of the tower, returns null if no targets are supplied</returns>
    public override Collider GetTarget(Collider[] pTargetsInRange, Vector3 pCenterPosition)
    {
        if (pTargetsInRange.Length == 0) return null;

        Collider closest = pTargetsInRange[0];
        float closestDistance = Vector3.SqrMagnitude(pCenterPosition - pTargetsInRange[0].gameObject.transform.position);

        for (int i = 1; i < pTargetsInRange.Length; i++)
        {
            float temp = Vector3.SqrMagnitude(pCenterPosition - pTargetsInRange[i].gameObject.transform.position);
            if (temp < closestDistance)
            {
                closest = pTargetsInRange[i];
                closestDistance = temp;
            }
        }

        return closest;
    }

    /// <param name="pTargetsInRange">A collider array of all targets to check</param>
    /// <param name="pCenterPosition">The tower's position to check the collider locations against</param>
    /// <param name="pTarget">'out' variable for the closest collider to the Center of the tower, will be null if no target was found</param>
    /// <returns>true if a target was found</returns>
    public override bool TryGetTarget(Collider[] pTargetsInRange, Vector3 pCenterPosition, out Collider pTarget)
    {
        pTarget = GetTarget(pTargetsInRange, pCenterPosition);

        if (pTarget == null)
            return false;
        else
            return true;
    }

}
