using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

public abstract class SpawnStrategyBase : ScriptableObject
{
    public abstract Queue<Tuple<EnemyScriptable, float>> GetSpawnOrder(EnemyGroup pGroup);
}
