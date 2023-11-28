using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject, I_Containable
{

    [SerializeField]
    private string _name = "Bob";
    [SerializeField]
    private GameObject _enemyModel;

    public AbstractMovementStrategy MovemenStrategy;

    public EnemyValues Values;

    public string GetName { get { return _name; } }

    public int CreationCost { get { return 0; } } //Maybe do something with this

    public GameObject GetModel
    {
        get { return _enemyModel; }
    }
}