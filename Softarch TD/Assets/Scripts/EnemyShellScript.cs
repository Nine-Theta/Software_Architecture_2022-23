using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShellScript : MonoBehaviour
{
    private EnemyScriptable _enemyData;
    private GameObject _model;

    public void InitEnemy(EnemyScriptable pEnemyData)
    {
        _enemyData = pEnemyData;
        _model = Instantiate<GameObject>(_enemyData.EnemyModel,this.transform);
        name = _enemyData.Name + " [ " + _enemyData.name + "]";
    }

}