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
    private int _upgradeRank = 0;
    [SerializeField]
    private int _upgradeMax;

    private bool activated = false;

    private Collider _currentTarget;

    private List<Collider> _targetsInRange =  new List<Collider>();


    public GameObject GetModel() { return _model; }
    public TowerValues GetCurrentValues() { return _runtimeValues; }
    public TowerValues GetNextUpgradeValues() { return _baseData.UpgradeValues[_upgradeRank]; }
    public int GetCurrentRank () { return _upgradeRank; }
    public bool CanUgrade() { return _upgradeRank + 1 < _upgradeMax; }

    public EventPublisher<Transform> TargetAcquired = new EventPublisher<Transform>();
    public EventPublisher TargetLost = new EventPublisher();

    public override I_Containable BaseData { get { return _baseData; } }

    public List<string> Debuffs = new List<string>();

    public override void Initialize(I_Containable pData)
    {
        _baseData = pData as TowerScriptable;
        _runtimeValues = _baseData.BaseValues;
        _upgradeMax = _baseData.UpgradeValues.Count;

        if (_model == null) _model = transform.GetChild(0).gameObject;
        _model.GetComponent<TowerModelController>().Initialize(this);

        if (_rangeCollider == null) _rangeCollider = gameObject.GetComponent<SphereCollider>();
        _rangeCollider.radius = _runtimeValues.Range;        
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(gameObject.transform.position, Vector3.up, _runtimeValues.Range, 2f);
    }

    private void CheckForNewTarget()
    {
        Collider target;

        if (_baseData.AttackStrategy.TryGetTarget(_targetsInRange.ToArray(), gameObject.transform.position, out target))
        {
            _currentTarget = target;
            TargetAcquired.Publish(target.transform);
            activated = true;
        }
        else
        {
            activated= false;
        }
    }

    private void AttackCurrentTarget()
    {
        _currentTarget.GetComponent<EnemyObject>().DamageEnemy(_runtimeValues.Damage);
        Debug.DrawLine(transform.position, _currentTarget.transform.position, Color.red, 2f);
    }

    [Button]
    public void TestUpgradeTower()
    {
        if (!CanUgrade()) return;

        _runtimeValues = _baseData.UpgradeValues[_upgradeRank];
        _upgradeRank += 1;

        _rangeCollider.radius = _runtimeValues.Range;

        Debug.Log("Tower Upgraded!");
    }

    public void Update()
    {
        if (activated && _cooldownTimer <= Time.time)
        {
            if (_targetsInRange.Count > 1)
                CheckForNewTarget();

            AttackCurrentTarget();
        }

        _cooldownTimer = Time.time + (1 / _runtimeValues.Cooldown);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        _targetsInRange.Add(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        _targetsInRange.Remove(other);

        if (other == _currentTarget)
        {
            TargetLost.Publish();
            CheckForNewTarget();
        }
    }
}
