using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public AbstractMovementStrategy MovemenStrategy;

    public string Name = "Bob";

    public EnemyValues Values;

    public GameObject EnemyModel;


    public EnemyScriptable()
    {
        
    }

    private void OnEnable()
    {

    }
}