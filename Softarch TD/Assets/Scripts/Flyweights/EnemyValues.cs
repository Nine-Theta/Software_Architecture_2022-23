using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enum for all the various enemy stats
/// </summary>
public enum EnemyStats
{
    /// <summary>Total health the enemy has</summary>
    HEALTH,
    /// <summary>Unmodified movementspeed of the Enemy</summary>
    SPEED,
    /// <summary>A flat reduction to any damage the enemy takes</summary>
    DEFENSE,
    /// <summary>A percentage reduction to the damage the enemy takes, after defense has been deducted</summary>
    RESISTANCE,
    /// <summary>How much damage the enemy does to the Objective</summary>
    ATTACK,
    /// <summary>How much reward the player gets when defeating the enemy</summary>
    REWARD
};

/// <summary>
/// All the stats an enemy has.
/// </summary>
/// <remarks>Contained in an <see cref="EnemyScriptable"/></remarks>
[Serializable]
public class EnemyValues
{
    [Tooltip("Total health the enemy has")]
    public float Health;

    [Tooltip("Unmodified movementspeed of the Enemy")]
    public float MovementSpeed;

    [Tooltip("A flat reduction to any damage the enemy takes")]
    public float Defense;

    [Range(-1.0f, 1.0f), Tooltip("A percentage reduction to the damage the enemy takes, after defense has been deducted")]
    public float Resistance;

    [Tooltip("How much damage the enemy does to the Objective")]
    public float AttackDamage;

    [Tooltip("How much reward the player gets when defeating the enemy")]
    public float Reward;

    /// <summary>
    /// All the stats an enemy has.
    /// </summary>
    /// <param name="pHealth">Total health the enemy has</param>
    /// <param name="pMovementSpeed">Unmodified movementspeed of the Enemy</param>
    /// <param name="pDefense">A flat reduction to any damage the enemy takes</param>
    /// <param name="pResistance">A percentage reduction to the damage the enemy takes, after defense has been deducted</param>
    /// <param name="pAttackDamage">How much damage the enemy does to the Objective</param>
    /// <param name="pReward">How much reward the player gets when defeating the enemy</param>
    public EnemyValues(float pHealth, float pMovementSpeed, float pDefense, float pResistance, float pAttackDamage, float pReward)
    {
        Health = pHealth;
        MovementSpeed = pMovementSpeed;
        Defense = pDefense;
        Resistance = pResistance;
        AttackDamage = pAttackDamage;
        Reward = pReward;
    }

    public EnemyValues(EnemyValues pOriginal) : this(pOriginal.Health, pOriginal.MovementSpeed, pOriginal.Defense, pOriginal.Resistance, pOriginal.AttackDamage, pOriginal.Reward) { }

    public float GetStat(EnemyStats pStat)
    {
        switch (pStat)
        {
            case EnemyStats.HEALTH:
                return Health;
            case EnemyStats.SPEED:
                return MovementSpeed;
            case EnemyStats.DEFENSE:
                return Defense;
            case EnemyStats.RESISTANCE:
                return Resistance;
            case EnemyStats.ATTACK:
                return AttackDamage;
            case EnemyStats.REWARD:
                return Reward;
            default: //Shouldn't ever trigger
                return Health;
        }
    }
}
