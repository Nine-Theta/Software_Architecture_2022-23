using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// For a group of enemies, determines what <see cref="SpawnStrategyBase"/> should be used to spawn them, and how long the delay is after the group has finished spawning.
/// </summary>
/// <remarks>Contained within a <see cref="WaveScriptable"/>, has a List of <see cref="EnemySpawnSettings"/></remarks>
[Serializable]
public class EnemyGroup
{
    public SpawnStrategyBase SpawnStrategy;
    [Tooltip("How long to delay for the next group, after this one is depleted of enemies")]
    public float GroupSpawnDelay = 0.0f;
    public List<EnemySpawnSettings> EnemyTypes;
}

