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
    public AbstractMovementStrategy MovemenStrategy;

    [SerializeField]
    private string _name = "Bob";

    public EnemyValues Values;

    public GameObject _containerObject;

    public string GetName { get { return _name; } }

    public GameObject GetContainerObject
    {
        get
        {
            if (_containerObject.GetComponent<EnemyObject>() == null)
            {
                Debug.LogError("Container Object for :" + this + " Is NULL or does not contain the proper script. Go fix it");
                return null;
            }
            else
                return _containerObject;
        }
    }
}