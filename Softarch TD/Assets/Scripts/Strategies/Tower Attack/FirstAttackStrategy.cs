using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// This strategy will make the <see cref="TowerObject"/> attack the <see cref="EnemyObject"/> that entered its range first
/// </summary>
[CreateAssetMenu(fileName = "FirstAttackStrategy", menuName = "Strategy/Attack/First")]
public class FirstAttackStrategy : AbstractAttackStrategy
{
    public override bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker)
    {
        if (pEnemies.Length == 0) return false;

        pAttacker.AttackTarget(pEnemies[0]);

        return true;
    }
}

