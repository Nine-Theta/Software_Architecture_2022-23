using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStats { HEALTH, SPEED, DEFENSE, RESISTANCE, ATTACK, REWARD };

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
}
