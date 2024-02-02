using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DebuffCommander
{
    [SerializeField]
    private List<AbstractDebuffCommand> _staticDebuffs = new List<AbstractDebuffCommand>();
    //private List<BleedDebuffCommand> _continousDebuffs;

    public DebuffCommander()
    {
        //_debuffs = new Dictionary<EnemyStats, List<DebuffCommand>>();
        foreach (EnemyStats stat in Enum.GetValues(typeof(EnemyStats)))
        {
            //_debuffs.Add(stat, new List<DebuffCommand>());
        }
    }

    public void AddDebuff(AbstractDebuffCommand pDebuff, DebuffType pType)
    {
        pDebuff.Execute();

        switch (pType)
        {
            default:
                _staticDebuffs.Add(pDebuff);
                break;
        }
    }

    public float GetDebuffedValue(EnemyStats pStat, float pValue)
    {
        return 0f;
    }

    public void TickDebuffs()
    {

    }
}
