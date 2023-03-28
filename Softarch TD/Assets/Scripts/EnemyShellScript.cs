using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;

public class EnemyShellScript : AbstractShell<EnemyScriptable>
{
    public void Initialize(EnemyScriptable pData)
    {


        //maybe do this in factory as well?
        //Data = Instantiate(pData);
        //Data.name = pData.name;
      
        //todo: do in factory;
        //_model = Instantiate<GameObject>(_enemyData.EnemyModel, this.transform);
        //name = _enemyData.Name + " [ " + _enemyData.name + "]";
        //_enemyData.Values.Health -= 1;
        
    }
}
