using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpawnFactory<T> : MonoBehaviour where T : ISpawnable 
{
    public virtual? void SpawnEnemy(T pSpawnable, Vector3 pPosition, Quaternion pRotation)
    {
        GameObject newEnemy = Instantiate(pSpawnable.GetTemplate, pPosition, pRotation);
        //newEnemy.GetComponent<EnemyShellScript>().InitEnemy(pEnemyData);
    }

}
