using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.AI;

[SelectionBase]
public class EnemyObject : AbstractContainerObject<EnemyScriptable>
{
    [SerializeField]
    private EnemyScriptable _baseData;
    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private EnemyValues _runtimeValues;

    public Vector3 TargetPos;


    public List<DebuffScriptable> ActiveDebuffs;

    public override EnemyScriptable BaseData { get { return _baseData; } }

    public override void Initialize(EnemyScriptable pData)
    {
        _baseData = pData;
        _runtimeValues = new EnemyValues(pData.Values);
    }

    [Button]
    public void Damag()
    {
        DamageEnemy(1);
    }

    [Button]
    public void Move()
    {
        this.GetComponent<NavMeshAgent>().SetDestination(TargetPos);
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

    public void ModifyStat(EnemyStats pStat, float pModifier, bool pFlatModifer = false)
    {
        switch (pStat)
        {
            case EnemyStats.HEALTH:
                if (pFlatModifer)
                {
                    _runtimeValues.Health += pModifier;
                    CheckForDeath();
                }
                else
                    _runtimeValues.Health *= pModifier;
                break;

            case EnemyStats.SPEED:
                if (pFlatModifer)
                    _runtimeValues.MovementSpeed += pModifier;
                else
                    _runtimeValues.MovementSpeed *= pModifier;
                break;

            case EnemyStats.DEFENSE:
                if (pFlatModifer)
                    _runtimeValues.Defense += pModifier;
                else
                    _runtimeValues.Defense *= pModifier;
                break;

            case EnemyStats.RESISTANCE:
                if (pFlatModifer)
                    _runtimeValues.Resistance += pModifier;
                else
                    _runtimeValues.Resistance *= pModifier;
                break;

            case EnemyStats.ATTACK:
                if (pFlatModifer)
                    _runtimeValues.AttackDamage += pModifier;
                else
                    _runtimeValues.AttackDamage *= pModifier;
                break;

            case EnemyStats.REWARD:
                if (pFlatModifer)
                    _runtimeValues.Reward += pModifier;
                else
                    _runtimeValues.Reward *= pModifier;
                break;

            default:
                throw new NotImplementedException();
        }
    }


    //Maybe implement TakeDamage as a Job?
    public void DamageEnemy(float pDamage)
    {
        _runtimeValues.Health -= Mathf.Max((pDamage - _runtimeValues.Defense) * (1 - _runtimeValues.Resistance), 0);
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (_runtimeValues.Health <= 0)
        {
            Debug.Log("Death to be implemented");
            Destroy(gameObject);
        }
    }
}
