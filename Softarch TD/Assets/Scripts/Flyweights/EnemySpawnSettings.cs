using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnSettings
{
    public EnemyScriptable EnemyType;

    public int SpawnCount;
    //[Tooltip("How long to wait for to spawn another object of the same type")]
    public float SpawnDelay;
}
