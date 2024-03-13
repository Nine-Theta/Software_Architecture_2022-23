using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This strategy will make the <see cref="TowerObject"/> attack whatever <see cref="EnemyObject"/> is currently the closest to it.
/// </summary>
[CreateAssetMenu(fileName = "ClosestAttackStrategy", menuName = "Strategy/Attack/Closest")]
public class ClosestAttackStrategy : AbstractAttackStrategy
{
    public override bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker)
    {
        if (pEnemies.Length == 0) return false;

        int closest = 0;
        float closestDistance = Vector3.SqrMagnitude(pEnemies[0].TargetPos - pEnemies[0].transform.position);

        for (int i = 1; i < pEnemies.Length; i++)
        {
            float temp = Vector3.SqrMagnitude(pEnemies[0].TargetPos - pEnemies[i].transform.position);
            if (temp < closestDistance)
            {
                closest = i;
                closestDistance = temp;
            }
        }

        pAttacker.AttackTarget(pEnemies[closest]);

        return true;
    }

}
