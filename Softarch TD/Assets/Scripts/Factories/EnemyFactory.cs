using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactory : AbstractInstanceFactory
{
    [SerializeField]
    private EnemyScriptable _enemy;

    [Button]
    public void TestSpawn()
    {
        CreateInstance(new Vector3());
    }

    public override AbstractContainerObject CreateInstance(Vector3 pPosition)
    {
        GameObject newEnemy = Instantiate(_enemy.GetContainerObject, pPosition, Quaternion.identity); //pPosition, pRotation);
        newEnemy.name = _enemy.GetName;

        EnemyObject instance = newEnemy.GetComponent<EnemyObject>();
        instance.Initialize(_enemy);

        return instance;
    }

    public override void SetContainable(I_Containable pEnemyScriptable)
    {
        SetEnemyVariant(pEnemyScriptable as EnemyScriptable);
    }

    public void SetEnemyVariant(EnemyScriptable pEnemyScriptable)
    {
        _enemy = pEnemyScriptable;
    }
}
