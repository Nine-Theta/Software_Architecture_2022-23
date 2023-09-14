using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClosestAttack", menuName = "AttackStrategy/Closest")]
public class ClosestAttackStrategy : AbstractAttackStrategy
{
    public override Collider GetTarget(Collider[] pTargetsInRange, Vector3 pCenterPosition)
    {
        Collider closest = pTargetsInRange[0];
        float closestDistance = Vector3.SqrMagnitude(pCenterPosition - pTargetsInRange[0].gameObject.transform.position);

        for(int i = 1; i < pTargetsInRange.Length; i++)
        {
            float temp = Vector3.SqrMagnitude(pCenterPosition - pTargetsInRange[i].gameObject.transform.position);
            if(temp < closestDistance)
            {
                closest = pTargetsInRange[i];
                closestDistance = temp;
            }
        }


        return closest;
    }

}
