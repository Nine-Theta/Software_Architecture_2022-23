using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies how many instances of a certain <see cref="EnemyScriptable"/> should be spawned, and how long the spawn delay is between individual instances
/// </summary>
[Serializable]
public class EnemySpawnSettings
{
    public EnemyScriptable EnemyType;

    public int SpawnCount = 1;
    //[Tooltip("How long to wait for to spawn another object of the same type")]
    public float SpawnDelay;
}
