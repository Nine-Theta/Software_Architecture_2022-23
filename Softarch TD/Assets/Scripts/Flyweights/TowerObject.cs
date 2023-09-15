using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TowerObject : MonoBehaviour
{
    [SerializeField]
    private TowerScriptable _baseData;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private Collider _rangeCollider;

    public void Initialize(TowerScriptable pData, GameObject pModel, Collider pCollider)
    {
        _baseData = pData;
        _model = pModel;
        _rangeCollider = pCollider;
    }


    [Button]
    public void TestAttackEnemy()
    {
        Debug.Log(gameObject.transform.position);
        Collider[] c = Physics.OverlapSphere(gameObject.transform.position, _baseData.Range, 1 << 10);
        Debug.Log(c.Length);
        if (c.Length > 0)
        {
            Collider t = _baseData.AttackStrategy.GetTarget(c, gameObject.transform.position);
            t.GetComponent<EnemyObject>().DamageEnemy(_baseData.Damage);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        Debug.Log("Enemy Entered Range");
    }
}
