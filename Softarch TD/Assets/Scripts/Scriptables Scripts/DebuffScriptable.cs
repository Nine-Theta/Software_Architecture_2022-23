using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebuffType { STATIC, CONTINUOUS }

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
