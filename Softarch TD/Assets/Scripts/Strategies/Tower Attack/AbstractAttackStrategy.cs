using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that serves as the base for attack strageies used by <see cref="TowerObject"/>s.
/// </summary>
/// <remarks>Contained in a <see cref="TowerScriptable"/></remarks>
public abstract class AbstractAttackStrategy : ScriptableObject
{
    public abstract bool AttackEnemies(EnemyObject[] pEnemies, TowerObject pAttacker);
}
