using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemySpawnSettings
{
    public EnemyScriptable EnemyType;
    public int EnemyCount;
    [Tooltip("How long to wait for to spawn another enemy of the same type")]
    public float SpawnDelay;
}
