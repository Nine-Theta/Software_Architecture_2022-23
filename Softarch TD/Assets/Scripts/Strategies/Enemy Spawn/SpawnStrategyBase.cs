using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Abstract ScriptableObject class that serves as the base for concrete spawn strategies.
/// </summary>
public abstract class SpawnStrategyBase : ScriptableObject
{
    public abstract Queue<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup);
}
