using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttackStrategy : ScriptableObject
{
    public abstract bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker);
}
