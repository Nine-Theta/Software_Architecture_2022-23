using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase, RequireComponent(typeof(SphereCollider))]
public class TowerObject : AbstractContainerObject
{
    [SerializeField]
    private TowerScriptable _baseData;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private SphereCollider _rangeCollider;

    [SerializeField]
    private TowerValues _runtimeValues;


    private float _cooldownTimer = 0;

    [SerializeField]
    private int _currentUpgradeRank = 0;
    [SerializeField]
    private int _upgradeMax;

    private bool activated = false;

    private Collider _currentTarget;

    private List<Collider> _targetsInRange = new List<Collider>();


    public GameObject GetModel() { return _model; }
    public TowerValues GetCurrentValues() { return _runtimeValues; }
    public TowerValues GetNextUpgradeValues() { return _baseData.TowerRankValues[Mathf.Min(_currentUpgradeRank+1, _upgradeMax)]; }
    public int GetCurrentRank() { return _currentUpgradeRank; }
    public bool CanUgrade() { return _currentUpgradeRank < _upgradeMax; }

    public EventPublisher<Transform> TargetAcquired = new EventPublisher<Transform>();
    public EventPublisher TargetLost = new EventPublisher();
    public EventPublisher FireAtTarget = new EventPublisher();

    private EventPublisher _targetDestroyed = new EventPublisher();

    public override I_Containable BaseData { get { return _baseData; } }

    public List<string> Debuffs = new List<string>();

    public override void Initialize(I_Containable pData, GameObject pTowerModel)
    {
        _baseData = pData as TowerScriptable;
        _runtimeValues = _baseData.TowerRankValues[0];
        _upgradeMax = _baseData.TowerRankValues.Count-1;

        _model = pTowerModel;
        _model.GetComponent<TowerModelController>().Initialize(this);

        if (_rangeCollider == null) _rangeCollider = gameObject.GetComponent<SphereCollider>();
        _rangeCollider.radius = _runtimeValues.Range;

        TargetLost.Subscribe(CheckForNewTarget);

        _targetDestroyed.Subscribe(OnTargetDestroyed);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(gameObject.transform.position, Vector3.up, _runtimeValues.Range, 2f);
    }

    //Automatically gets called by the TargetLost event as well
    private void CheckForNewTarget()
    {
        Collider target;

        if (_baseData.AttackStrategy.TryGetTarget(_targetsInRange.ToArray(), gameObject.transform.position, out target))
        {
            _currentTarget = target;

            _targetDestroyed.Unsubscribe(OnTargetDestroyed);
            _targetDestroyed = target.GetComponent<EnemyObject>().EnemyDestroyed;
            _targetDestroyed.Subscribe(OnTargetDestroyed);

            TargetAcquired.Publish(target.transform);
            activated = true;
        }
        else
        {
            activated = false;
        }
    }

    private void AttackCurrentTarget()
    {
        Debug.Log("attacking: " + _currentTarget.name);
        _currentTarget.GetComponent<EnemyObject>().DamageEnemy(_runtimeValues.Damage);
        FireAtTarget.Publish();
        Debug.DrawLine(transform.position, _currentTarget.transform.position, Color.red, 3f);
    }

    private void OnTargetDestroyed()
    {
        _targetDestroyed.Unsubscribe(OnTargetDestroyed);
        _targetsInRange.Remove(_currentTarget);
        activated = false;
        TargetLost.Publish();
    }

    public bool TryUpgradeTower()
    {
        if (!CanUgrade())
            return false;

        _currentUpgradeRank += 1;
        _runtimeValues = _baseData.TowerRankValues[_currentUpgradeRank];

        _rangeCollider.radius = _runtimeValues.Range;

        return true;
    }

    public bool TryDownGradeTower()
    {
        if (_currentUpgradeRank <= 0)
            return false;

        _currentUpgradeRank -= 1;

        _runtimeValues = _baseData.TowerRankValues[_currentUpgradeRank];

        _rangeCollider.radius = _runtimeValues.Range;

        return true;
    }

    [Button]
    public void TestUpgradeTower()
    {
        TryUpgradeTower();

        Debug.Log("Tower Upgraded!");
    }

    public void Update()
    {
        if (activated && _cooldownTimer <= Time.time)
        {
            if (_targetsInRange.Count > 1)
                CheckForNewTarget();

            AttackCurrentTarget();

            _cooldownTimer = Time.time + (1 / _runtimeValues.Cooldown);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        _targetsInRange.Add(other);
        CheckForNewTarget();
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        _targetsInRange.Remove(other);

        if (other == _currentTarget)
        {
            TargetLost.Publish();
        }
    }
}
