using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public string MovemenStrategy = "todo";

    public string Name = "Bob";

    public EnemyValues Values;

    public GameObject EnemyModel;

    public List<DebuffScriptable> ActiveDebuffs;

    private Dictionary<EnemyStats, float> _stats = new Dictionary<EnemyStats, float>();


    public EnemyScriptable()
    {
        /*
        _stats.Add(EnemyStats.HEALTH, Health);
        _stats.Add(EnemyStats.SPEED, MovementSpeed);
        _stats.Add(EnemyStats.DEFENSE, Defense);
        _stats.Add(EnemyStats.RESISTANCE, Resistance);
        _stats.Add(EnemyStats.ATTACK, AttackDamage);
        _stats.Add(EnemyStats.REWARD, Reward);
        */
    }

    private void OnEnable()
    {
        /*
        Health = _baseHealth;
        MovementSpeed = _baseMovementSpeed;
        Defense = _baseDefense;
        Resistance = _baseResistance;
        AttackDamage = _baseAttackDamage;
        Reward = _baseReward;
        */
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
        //if (pDamage < Defense) return Health;

        //Health -= (pDamage - Defense) * (1 - Resistance);
        //return Health;

        return 0;
    }
}