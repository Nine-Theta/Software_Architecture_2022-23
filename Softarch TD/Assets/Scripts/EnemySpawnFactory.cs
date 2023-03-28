using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnFactory : AbstractSpawnFactory<EnemyScriptable>
{
    EnemyScriptable enemy;

    //todo:
    /* spawn group of object from list
     * the above but according to spawnstrat
     * should be declared in abstract
     * */

    public void SpawnEnemyGroup(EnemyGroupScriptable pGroup, Vector3 pPosition, Quaternion pRotation)
    {
        
    }

    public override void Spawn(Vector3 pPosition, Quaternion pRotation)
    {
        AbstractShell<EnemyScriptable> newEnemy = SpawnObject(pPosition, pRotation);
        newEnemy.Data = Instantiate(enemy);
        newEnemy.Data.name = enemy.name;
    }

    protected override AbstractShell<EnemyScriptable> SpawnObject(Vector3 pPosition, Quaternion pRotation)
    {
        return base.SpawnObject(pPosition, pRotation);   
    }
}
