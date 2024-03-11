using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Scriptable Object which serves as the container for a single enemy wave.
/// </summary>
/// <remarks>Contains a List of <see cref="EnemyGroup"/>s </remarks>
[CreateAssetMenu(fileName = "WaveSettings", menuName = "ScriptableObjects/Wave")]
public class WaveScriptable : ScriptableObject
{
    public List<EnemyGroup> EnemyGroups;
}
