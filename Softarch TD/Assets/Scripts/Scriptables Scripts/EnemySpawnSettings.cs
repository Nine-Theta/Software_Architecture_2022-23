using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnSettings//<T> //used to be a struct
{
    //public T SpawnType;
    [Min(1)]
    public int SpawnCount;
    [Tooltip("How long to wait for to spawn another object of the same type")]
    public float SpawnDelay;
}
