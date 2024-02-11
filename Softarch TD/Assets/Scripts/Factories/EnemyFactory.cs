using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : AbstractInstanceFactory
{
    [SerializeField]
    private EnemyScriptable _enemy;

    [SerializeField]
    private EnemyObject _enemyObject;

    public EnemyScriptable EnemyVariant
    {
        get { return _enemy; }
        set { _enemy = value; }
    }

    public override I_Containable Containable
    {
        get { return _enemy; }
        set { _enemy = value is EnemyScriptable ? value as EnemyScriptable : throw new System.ArgumentException("Incorrect Containable", "EnemyFactory" ); }
    }
    public AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        return CreateInstance(pPosition, Quaternion.identity);
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition, Quaternion pRotation)
    {
        GameObject newEnemy = Instantiate(_enemyObject.gameObject, pPosition, pRotation);
        GameObject model = Instantiate(_enemy.GetModel,_enemy.GetModel.transform.position + pPosition, pRotation, newEnemy.transform);
        newEnemy.name = _enemy.GetName;

        EnemyObject instance = newEnemy.GetComponent<EnemyObject>();
        instance.Initialize(_enemy, model);

        return instance;
    }

    public void SetEnemyVariant(EnemyScriptable pEnemyScriptable)
    {
        _enemy = pEnemyScriptable;
    }
}
