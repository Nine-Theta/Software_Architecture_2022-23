using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abstract class that serves as the base for concrete implementations of enemy movement using the strategy pattern.
/// </summary>
public abstract class AbstractMovementStrategy : ScriptableObject
{
    public abstract void Initialize(EnemyObject pEnemy, float pMoveSpeed);

    public abstract void MoveTo(EnemyObject pEnemy, Vector3 pDestiniation);

    public abstract void ChangeMoveSpeed(EnemyObject pEnemy, float pMoveSpeed);
}
