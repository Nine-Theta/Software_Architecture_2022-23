using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;

public class EnemyShellScript : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptable _enemyData;
    private GameObject _model;

    public void InitEnemy(EnemyScriptable pEnemyData)
    {
        _enemyData = Instantiate(pEnemyData);
        _enemyData.name = pEnemyData.name;
      
        _model = Instantiate<GameObject>(_enemyData.EnemyModel, this.transform);
        name = _enemyData.Name + " [ " + _enemyData.name + "]";
        _enemyData.Health -= 1;
    }
}