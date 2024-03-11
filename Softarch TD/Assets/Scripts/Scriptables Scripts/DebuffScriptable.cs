using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The main types of debuff that are can be inflicted on Enemies.
/// </summary>
public enum DebuffType
{
    /// <summary>Debuff will only trigger once, when it's applied</summary>
    STATIC,
    /// <summary>Debuff will trigger continuously</summary>
    CONTINUOUS
}

/// <summary>
/// Code for a ScriptableObject instance that functions as a Debuff applied to enemies by towers.
/// </summary>
/// <remarks>Contained in a <see cref="TowerScriptable"/> and applied to an <see cref="EnemyObject"/>. Managed on the enemy side by a <see cref="DebuffHandler"/>.</remarks>
[CreateAssetMenu(fileName = "Debuff", menuName = "ScriptableObjects/Debuff"), Serializable]
public class DebuffScriptable : ScriptableObject
{
    public EnemyStats AffectedStat;

    public DebuffType DebuffVariant;

    public float Strength = 1f;
    public bool UseAsMultiplier = false;

    public float Duration = 1f;

    [Tooltip("How often the effect triggers per second (Continuous only)"), ShowIf("DebuffVariant", DebuffType.CONTINUOUS)]
    public float TickSpeed = 1f;

    public void ApplyDebuff (EnemyObject pEnemy)
    {
        float mod = Strength;

        if (UseAsMultiplier)
            mod *= pEnemy.RuntimeValues.GetStat(AffectedStat);

        pEnemy.ModifyStat(AffectedStat, -mod);
    }

    public void RemoveDebuff(EnemyObject pEnemy)
    {
        float mod = Strength;

        if (UseAsMultiplier)
            mod *= pEnemy.RuntimeValues.GetStat(AffectedStat);

        pEnemy.ModifyStat(AffectedStat, mod);

    }
}
