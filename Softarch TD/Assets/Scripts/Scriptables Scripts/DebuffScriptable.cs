using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Debuff", menuName = "ScriptableObjects/Debuff"), Serializable]
public class DebuffScriptable : ScriptableObject
{
    public EnemyStats AffectedStat;

    [Tooltip("Subtract reduces stat by strength, Multiply multiplies stat with strength, bleed reduces stat with strength every Tick ")]
    public DebuffType Type;

    public float Strength = 1f;

    public float Duration = 1f;

    [Tooltip("How often the effect triggers per second (Bleed only)")]
    public float TickSpeed = 1f;

    public AbstractDebuffCommand CreateDebuffCommand(EnemyValues pEnemyValues)
    {
        switch (Type)
        {
            case DebuffType.SUBTRACT:
                return new SubtractDebuffCommand(Strength, Duration, pEnemyValues.GetStat(AffectedStat));
            default:
                return new SubtractDebuffCommand(Strength, Duration, pEnemyValues.GetStat(AffectedStat));

        }
    }
}
