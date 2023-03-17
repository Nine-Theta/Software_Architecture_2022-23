using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebuffScriptable : ScriptableObject
{
    public EnemyStats StatToAffect;

    public DebuffStrategy DebuffStrategy;

    public float DebuffStrength;
    public float DebuffDuration;

    public void OnEnable()
    {
        Debug.Log("Debuff Enabled");
    }

    public void OnDisable()
    {
        Debug.Log("Debuff Disabled");
    }
}
