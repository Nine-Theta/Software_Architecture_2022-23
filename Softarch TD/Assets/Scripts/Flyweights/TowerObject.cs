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
    private GameObject _template;
    [SerializeField]
    private SphereCollider _rangeCollider;

    [SerializeField]
    private TowerValues _runtimeValues;

    private float _cooldownTimer = 0;

    private int _upgradeRank = 0;
    private int _upgradeMax;

    private bool activated = false;

    public override I_Containable BaseData { get { return _baseData; } }

    public List<string> Debuffs = new List<string>();

    public override void Initialize(I_Containable pData)
    {
        _baseData = pData as TowerScriptable;
        _runtimeValues = _baseData.BaseValues;
        _upgradeMax = _baseData.UpgradeValues.Count;

        if (_template == null) _template = transform.GetChild(0).gameObject;
        if (_rangeCollider == null) _rangeCollider = gameObject.GetComponent<SphereCollider>();
        _rangeCollider.radius = _runtimeValues.Range;
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(gameObject.transform.position, Vector3.up, _runtimeValues.Range, 2f);
    }


    [Button]
    public void TestAttackEnemy()
    {
        //Debug.Log(gameObject.transform.position);
        Collider[] c = Physics.OverlapSphere(gameObject.transform.position, _runtimeValues.Range, 1 << 10);
        Debug.Log(c.Length);
        if (c.Length > 0)
        {
            Collider t = _baseData.AttackStrategy.GetTarget(c, gameObject.transform.position);
            t.GetComponent<EnemyObject>().DamageEnemy(_runtimeValues.Damage);
            Debug.DrawLine(transform.position, t.transform.position,Color.red, 2f);
        }
    }

    [Button]
    public void TestUpgradeTower()
    {
        if (!CanUgrade()) return;

        _upgradeRank += 1;
        _runtimeValues = _baseData.UpgradeValues[_upgradeRank];

        Debug.Log("Tower Upgraded!");
    }

    public bool CanUgrade()
    {
        return _upgradeRank < _upgradeMax;
    }

    public void Update()
    {

        if (activated && _cooldownTimer <= Time.time)
        {
            Debug.Log("TowerCall");
            Collider[] c = Physics.OverlapSphere(gameObject.transform.position, _runtimeValues.Range, 1 << 10);
            if (c.Length > 0)
            {
                TestAttackEnemy();
                _cooldownTimer = Time.time + (1 / _runtimeValues.Cooldown);
                Debug.Log("Pew! at: " + Time.time + " next pew at: " + _cooldownTimer);                
            }
            else
            {
                Debug.Log("no enemies in range");
                activated = false;
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        activated = true;
        Debug.Log("Enemy Entered Range");
    }
}
