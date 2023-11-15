using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovementStrategy : ScriptableObject
{
    public abstract void Initialize(EnemyObject pEnemy, float pMoveSpeed);

    public abstract void MoveTo(EnemyObject pEnemy, Vector3 pDestiniation);
}
