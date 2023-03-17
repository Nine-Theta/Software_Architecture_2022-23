using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public enum DebuffStrategy { REDUCEMAX, REDUCECURRENT, BLEED }

public abstract class DebuffStrategyBase
{
    public abstract void StartDebuff(EnemyScriptable pEnemy, EnemyStats pStat);
    public abstract void StopDebuff(EnemyScriptable pEnemy, EnemyStats pStat);

}

public class ReduceMaxStrategy : DebuffStrategyBase
{
    public override void StartDebuff(EnemyScriptable pEnemy, EnemyStats pStat)
    {
        
    }

    public override void StopDebuff(EnemyScriptable pEnemy, EnemyStats pStat)
    {
        
    }
}

/*
public class ReduceCurrentStrategy : DebuffStrategyBase
{
    public override void DebuffEnemy(EnemyScriptable pEnemy, EnemyStats pStat)
    {

    }

    public override void RemoveDebuff(EnemyScriptable pEnemy, EnemyStats pStat)
    {

    }
}


public class BleedStrategy : DebuffStrategyBase
{
    public override void DebuffEnemy(EnemyScriptable pEnemy, EnemyStats pStat)
    {

    }

    public override void RemoveDebuff(EnemyScriptable pEnemy, EnemyStats pStat)
    {

    }
}
*/
