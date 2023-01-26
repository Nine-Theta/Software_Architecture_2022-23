using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyGroupScriptable", menuName = "ScriptableObjects/EnemyGroup")]
public class EnemyGroupScriptable : ScriptableObject
{
    public SpawnStrategyBase SpawnStrategy;
    [Tooltip("How long to delay for the next group, after this one is depleted of enemies")]
    public float GroupSpawnDelay = 0.0f;
    public List<EnemySpawnSettings> EnemyTypes;
}

