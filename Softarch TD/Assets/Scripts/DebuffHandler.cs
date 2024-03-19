using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used by <see cref="EnemyObject"/>s to handle the <see cref="DebuffScriptable"/>s they receive from <see cref="TowerObject"/>s.
/// </summary>
[Serializable]
public class DebuffHandler
{
    [SerializeField]
    private Dictionary<DebuffScriptable, float> _staticDebuffs = new Dictionary<DebuffScriptable, float>();

    private Dictionary<DebuffScriptable, Vector2> _continuousDebuffs = new Dictionary<DebuffScriptable, Vector2>();

    private EnemyObject _enemy;

    public DebuffHandler(EnemyObject pEnemy)
    {
        _enemy = pEnemy;
    }

    public void AddDebuff(DebuffScriptable pDebuff)
    {

        switch (pDebuff.DebuffVariant)
        {
            case DebuffType.CONTINUOUS:
                if (_continuousDebuffs.ContainsKey(pDebuff))
                {
                    _continuousDebuffs[pDebuff].Set(_continuousDebuffs[pDebuff].x, pDebuff.Duration + Time.time);
                    Debug.Log("Debuff already applied, resetting duration to: " + _continuousDebuffs[pDebuff]);
                }
                else
                {
                    _continuousDebuffs.Add(pDebuff, new Vector2(pDebuff.TickSpeed + Time.time, pDebuff.Duration + Time.time));
                    pDebuff.ApplyDebuff(_enemy);
                    Debug.Log("Applying debuff [" + pDebuff.ToString() + "] with strength: (" + pDebuff.Strength + ") for {" + pDebuff.Duration + "}");
                }
                break;

            default:
                if (_staticDebuffs.ContainsKey(pDebuff))
                {
                    _staticDebuffs[pDebuff] = pDebuff.Duration + Time.time;
                    Debug.Log("Debuff already applied, resetting duration to: " + _staticDebuffs[pDebuff]);
                }
                else
                {
                    _staticDebuffs.Add(pDebuff, pDebuff.Duration + Time.time);
                    pDebuff.ApplyDebuff(_enemy);
                    Debug.Log("Applying debuff [" + pDebuff.ToString() + "] with strength: (" + pDebuff.Strength + ") for {" + pDebuff.Duration + "}");
                }
                break;
        }
    }

    public void TickDebuffs()
    {
        foreach (DebuffScriptable debuff in _staticDebuffs.Keys)
        {
            if (_staticDebuffs[debuff] < Time.time)
            {
                debuff.RemoveDebuff(_enemy);
                _staticDebuffs.Remove(debuff);
            }
        }

        foreach (DebuffScriptable debuff in _continuousDebuffs.Keys)
        {
            if (_continuousDebuffs[debuff].x < Time.time)
            {
                debuff.ApplyDebuff(_enemy);
                _continuousDebuffs[debuff].Set(debuff.TickSpeed, _continuousDebuffs[debuff].y);
            }

            if (_continuousDebuffs[debuff].y < Time.time)
            {
                debuff.RemoveDebuff(_enemy);
                _continuousDebuffs.Remove(debuff);
            }
        }
    }
}
