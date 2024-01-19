using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

[SelectionBase]
public class EnemyObject : AbstractContainerObject
{
    [SerializeField]
    private EnemyScriptable _baseData;
    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private EnemyValues _runtimeValues;

    public EnemyValues RuntimeValues { get { return _runtimeValues; } }
    [Space(5)]

    [SerializeField]
    private Slider _healthVisual;
    private float _healthVisualMult;

    public Vector3 TargetPos;
    public GameObject GetModel() { return _model; }

    public List<DebuffScriptable> ActiveDebuffs;

    public EventPublisher<EnemyObject> EnemyDestroyed = new EventPublisher<EnemyObject>();

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData, GameObject pEnemyModel)
    {
        _baseData = pData as EnemyScriptable;
        _runtimeValues = new EnemyValues(_baseData.Values);

        _model = pEnemyModel;

        _baseData.MovemenStrategy.Initialize(this, _runtimeValues.MovementSpeed);

        _healthVisualMult = 1 / _baseData.Values.Health;
    }

    public void Move()
    {
        _baseData.MovemenStrategy.MoveTo(this, TargetPos);
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
        _healthVisual.value = _runtimeValues.Health * _healthVisualMult;
        CheckForDeath();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            //Debug.Log("Base Reached!");
            other.GetComponentInParent<BaseManager>().DamageBase(_runtimeValues.AttackDamage);
            EnemyDestroyed.Publish(this);
            Destroy(gameObject);
        }
    }
    
    private void CheckForDeath()
    {
        if (_runtimeValues.Health <= 0)
        {
            Debug.Log("Death to be implemented");
            EnemyDestroyed.Publish(this);
            Destroy(gameObject);
        }
    }
}
