using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This strategy will make the <see cref="TowerObject"/> attack the <see cref="EnemyObject"/> that entered its range last
/// </summary>
[CreateAssetMenu(fileName = "LastAttackStrategy", menuName = "Strategy/Attack/Last")]
public class LastAttackStrategy : AbstractAttackStrategy
{
    public override bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker)
    {
        if (pEnemies.Length == 0) return false;

        pAttacker.AttackTarget(pEnemies[pEnemies.Length - 1]);

        return true;
    }
}

