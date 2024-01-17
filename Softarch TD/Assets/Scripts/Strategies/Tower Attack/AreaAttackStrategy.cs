using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackStrategy", menuName = "Strategy/Attack/AreaOfEffect")]
public class AreaAttackStrategy : AbstractAttackStrategy
{
    public override bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker)
    {
        if (pEnemies.Length == 0) return false;

        for(int i = 0; i < pEnemies.Length; i++)
        {
            pAttacker.AttackTarget(pEnemies[i]);
        }

        return true;
    }
}

