using System;
using UnityEngine;

/// <summary>
/// This script handles all the functionality of an enemy instance.
/// It moves using a variant of <see cref="AbstractMovementStrategy"/> contained in its <see cref="EnemyScriptable"/>, which also has its instantiation values.
/// </summary>
/// <remarks>It is instantiated by an <see cref="EnemyFactory"/></remarks> 
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
    private HealthbarVisual _healthbarVisual;

    private DebuffHandler _debuffHandler;

    public Vector3 TargetPos;


    public EventPublisher<EnemyObject> EnemyDestroyed = new EventPublisher<EnemyObject>();
    public EventPublisher<float> EnemyDamaged = new EventPublisher<float>();

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData, GameObject pEnemyModel)
    {
        _baseData = pData as EnemyScriptable;
        _runtimeValues = new EnemyValues(_baseData.Values);

        _model = pEnemyModel;

        _debuffHandler = new DebuffHandler(this);

        _baseData.MovementStrategy.Initialize(this, _runtimeValues.MovementSpeed);

        _healthbarVisual.Initialize(_baseData.Values.Health);
        EnemyDamaged.Subscribe(_healthbarVisual.UpdateHealth);
    }

    public void Move()
    {
        _baseData.MovementStrategy.MoveTo(this, TargetPos);
    }
    public GameObject GetModel() { return _model; }

    public void ApplyDebuff(DebuffScriptable pDebuff)
    {
        _debuffHandler.AddDebuff(pDebuff);
        Debug.Log("Debuff applied Enemy: " + pDebuff.ToString());

    }

    public void ModifyStat(EnemyStats pStat, float pModifier)
    {
        switch (pStat)
        {
            case EnemyStats.HEALTH:
                _runtimeValues.Health += pModifier;
                CheckForDeath();
                break;

            case EnemyStats.SPEED:
                _runtimeValues.MovementSpeed += pModifier;
                _baseData.MovementStrategy.ChangeMoveSpeed(this, _runtimeValues.MovementSpeed);
                break;

            case EnemyStats.DEFENSE:
                _runtimeValues.Defense += pModifier;
                break;

            case EnemyStats.RESISTANCE:
                _runtimeValues.Resistance += pModifier;
                break;

            case EnemyStats.ATTACK:
                _runtimeValues.AttackDamage += pModifier;
                break;

            case EnemyStats.REWARD:
                _runtimeValues.Reward += pModifier;
                break;

            default:
                throw new NotImplementedException();
        }
    }


    //Maybe implement TakeDamage as a Job?
    public void DamageEnemy(float pDamage)
    {
        _runtimeValues.Health -= Mathf.Max((pDamage - _runtimeValues.Defense) * (1 - _runtimeValues.Resistance), 0);
        EnemyDamaged.Publish(_runtimeValues.Health);
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
            EnemyDestroyed.Publish(this);
            FindObjectOfType<GameplayManager>().Credits += (int)_runtimeValues.Reward; //Last-minute addition, don't have time for a proper implementation anymore
            Destroy(gameObject);
        }
    }
}
