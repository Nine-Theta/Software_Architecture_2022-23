using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase, RequireComponent(typeof(Collider))]
public class TowerObject : AbstractContainerObject<TowerScriptable>
{
    [SerializeField]
    private TowerScriptable _baseData;
    [SerializeField]
    private GameObject _template;
    [SerializeField]
    private Collider _rangeCollider;

    private float _cooldownTimer = 0;

    private bool activated = false;

    public override TowerScriptable BaseData { get { return _baseData; } }

    public override void Initialize(TowerScriptable pData)
    {
        _baseData = pData;
        if (_template == null) _template = transform.GetChild(0).gameObject;
        if (_rangeCollider == null) _rangeCollider = gameObject.GetComponent<Collider>();
    }


    [Button]
    public void TestAttackEnemy()
    {
        //Debug.Log(gameObject.transform.position);
        Collider[] c = Physics.OverlapSphere(gameObject.transform.position, _baseData.Range, 1 << 10);
        Debug.Log(c.Length);
        if (c.Length > 0)
        {
            Collider t = _baseData.AttackStrategy.GetTarget(c, gameObject.transform.position);
            t.GetComponent<EnemyObject>().DamageEnemy(_baseData.Damage);
            Debug.DrawLine(transform.position, t.transform.position,Color.red, 2f);
        }
    }

    public void Update()
    {

        if (activated && _cooldownTimer <= Time.time)
        {
            Debug.Log("TowerCall");
            Collider[] c = Physics.OverlapSphere(gameObject.transform.position, _baseData.Range, 1 << 10);
            if (c.Length > 0)
            {
                TestAttackEnemy();
                _cooldownTimer = Time.time + (1 / _baseData.Cooldown);
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
