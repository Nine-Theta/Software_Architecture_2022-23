using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[SelectionBase]
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

    private List<EnemyObject> _targetsInRange = new List<EnemyObject>();


    public GameObject GetModel() { return _model; }
    public TowerValues GetCurrentValues() { return _runtimeValues; }
    public TowerValues GetNextUpgradeValues() { return _baseData.TowerRankValues[Mathf.Min(_currentUpgradeRank + 1, _upgradeMax)]; }
    public int GetCurrentRank() { return _currentUpgradeRank; }
    public bool CanUgrade() { return _currentUpgradeRank < _upgradeMax; }

    public EventPublisher<Transform> TargetAcquired = new EventPublisher<Transform>();

    public override I_Containable BaseData { get { return _baseData; } }

    public override void Initialize(I_Containable pData, GameObject pTowerModel)
    {
        _baseData = pData as TowerScriptable;

        _model = pTowerModel;
    }

    private void Start()
    {
        _runtimeValues = _baseData.TowerRankValues[0];
        _upgradeMax = _baseData.TowerRankValues.Count - 1;

        _model.GetComponent<TowerModelController>().Initialize(this);

        if (_rangeCollider == null) _rangeCollider = gameObject.GetComponentInChildren<SphereCollider>();
        _rangeCollider.radius = _runtimeValues.Range;
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(gameObject.transform.position, Vector3.up, _runtimeValues.Range, 2f);
    }

    public void AttackTarget(EnemyObject pEnemy)
    {
        pEnemy.DamageEnemy(_runtimeValues.Damage);
        TargetAcquired.Publish(pEnemy.transform);
        Debug.DrawLine(transform.position, pEnemy.transform.position, Color.red, 3f);

        if (_baseData.Debuffs.Count >= 1)
            ApplyDebuffs(pEnemy);
    }

    private void ApplyDebuffs(EnemyObject pEnemy)
    {
        for (int i = 0; i < _baseData.Debuffs.Count; i++)
        {
            pEnemy.ApplyDebuff(_baseData.Debuffs[i]);
            Debug.Log("Debuff applied tower: " + _baseData.Debuffs[i].ToString());
        }
    }

    private void RemoveTargetFromList(EnemyObject pTarget)
    {
        _targetsInRange.Remove(pTarget);
        pTarget.EnemyDestroyed.Unsubscribe(RemoveTargetFromList);
    }

    public bool TryUpgradeTower()
    {
        if (!CanUgrade())
            return false;

        _currentUpgradeRank += 1;
        _runtimeValues = _baseData.TowerRankValues[_currentUpgradeRank];

        _rangeCollider.radius = _runtimeValues.Range;

        GameObject newModel = Instantiate(_baseData.GetRankModel(_currentUpgradeRank), _model.transform.position, _model.transform.rotation, transform);
        Destroy(_model);
        newModel.GetComponent<TowerModelController>().Initialize(this);
        _model = newModel;

        return true;
    }

    public bool TryDownGradeTower()
    {
        if (_currentUpgradeRank <= 0)
            return false;

        _currentUpgradeRank -= 1;

        _runtimeValues = _baseData.TowerRankValues[_currentUpgradeRank];

        _rangeCollider.radius = _runtimeValues.Range;

        _model = Instantiate(_baseData.GetRankModel(_currentUpgradeRank));
        _model.GetComponent<TowerModelController>().Initialize(this);

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
            activated = _baseData.AttackStrategy.AttackEnemies(_targetsInRange.ToArray(), this);

            _cooldownTimer = Time.time + (1 / _runtimeValues.Cooldown);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        activated = true;

        _targetsInRange.Add(other.GetComponent<EnemyObject>());
        other.GetComponent<EnemyObject>().EnemyDestroyed.Subscribe(RemoveTargetFromList);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        _targetsInRange.Remove(other.GetComponent<EnemyObject>());
        other.GetComponent<EnemyObject>().EnemyDestroyed.Unsubscribe(RemoveTargetFromList);
    }

    public void OnDestroy()
    {
        TargetAcquired.UnsubscribeAll();

        for (int i = 0; i < _targetsInRange.Count; i++)
        {
            _targetsInRange[i].EnemyDestroyed.Unsubscribe(RemoveTargetFromList);
        }
    }
}
