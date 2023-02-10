using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSettings", menuName = "ScriptableObjects/Wave")]
public class WaveScriptable : ScriptableObject
{
    public List<EnemyGroupScriptable> Wave;

}
