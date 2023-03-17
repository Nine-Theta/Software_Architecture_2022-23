using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyStats { HEALTH, SPEED, DEFENSE, RESISTANCE, ATTACK, REWARD };

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public string MovemenStrategy = "todo";

    public string Name = "Bob";

    [SerializeField, Tooltip("Total health the enemy has")]
    private float _baseHealth = 10.0f;

    [SerializeField, Tooltip("Unmodified movementspeed of the Enemy")]
    private float _baseMovementSpeed = 1.0f;

    [SerializeField, Tooltip("A flat reduction to any damage the enemy takes")]
    private float _baseDefense = 0.0f;

    [SerializeField, Range(-1.0f, 1.0f), Tooltip("A percentage reduction to the damage the enemy takes, after defense has been deducted")]
    private float _baseResistance = 0.0f;

    [SerializeField, Tooltip("How much damage the enemy does to the Objective")]
    private float _baseAttackDamage = 1.0f;

    [SerializeField, Tooltip("How much reward the player gets when defeating the enemy")]
    private float _baseReward = 1.0f;

    public GameObject EnemyModel;


    [Header("Runtime Values")]
    public float Health = 10.0f;
    public float MovementSpeed = 1.0f;
    public float Defense = 0.0f;
    public float Resistance = 0.0f;
    public float AttackDamage = 1.0f;
    public float Reward = 1.0f;

    public List<DebuffScriptable> ActiveDebuffs;

    private Dictionary<EnemyStats, float> _stats = new Dictionary<EnemyStats, float>();


    public EnemyScriptable()
    {
        _stats.Add(EnemyStats.HEALTH, Health);
        _stats.Add(EnemyStats.SPEED, MovementSpeed);
        _stats.Add(EnemyStats.DEFENSE, Defense);
        _stats.Add(EnemyStats.RESISTANCE, Resistance);
        _stats.Add(EnemyStats.ATTACK, AttackDamage);
        _stats.Add(EnemyStats.REWARD, Reward);
    }

    private void OnEnable()
    {
        Health = _baseHealth;
        MovementSpeed = _baseMovementSpeed;
        Defense = _baseDefense;
        Resistance = _baseResistance;
        AttackDamage = _baseAttackDamage;
        Reward = _baseReward;

        ActiveDebuffs = new List<DebuffScriptable>();
    }


    public void ApplyDebuff(DebuffScriptable pDebuff)
    {
        if (ActiveDebuffs.Contains(pDebuff))
        {
            ActiveDebuffs.Find(item => item.name == pDebuff.name).DebuffDuration = pDebuff.DebuffDuration;
        }
        else
        {
            ActiveDebuffs.Add(Instantiate(pDebuff));
        }
    }

    public void RemoveDebuff(DebuffScriptable pDebuff)
    {
        ActiveDebuffs.Remove(pDebuff);
    }

    public void ModifyStat(EnemyStats pStat, float pModifier)
    {
        _stats[pStat] *= pModifier;
    }

    //Maybe implement TakeDamage as a Job?
    public float TakeDamage(float pDamage)
    {
        if (pDamage < Defense) return Health;

        Health -= (pDamage - Defense) * (1 - Resistance);
        return Health;
    }
}