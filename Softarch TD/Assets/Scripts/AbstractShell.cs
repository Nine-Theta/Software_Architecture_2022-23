using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ShellObject<T> : MonoBehaviour where T : ScriptableObject
{
    public T Data;

    public GameObject Model;
}
