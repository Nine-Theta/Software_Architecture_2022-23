using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private void HandleGroup(EnemyGroupScriptable pGroup)
    {
        pGroup.SpawnStrategy.SpawnGroup(pGroup);
    }
}
