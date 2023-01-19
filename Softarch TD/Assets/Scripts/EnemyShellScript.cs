using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class EnemyShellScript : MonoBehaviour
{
    private EnemyScriptable _enemyData;

    public void InitEnemy(EnemyScriptable pEnemyData)
    {
        _enemyData = pEnemyData;
        GetComponent<MeshFilter>().mesh = _enemyData.Mesh;
        name = _enemyData.Name + " [Enemy]";
    }

}
