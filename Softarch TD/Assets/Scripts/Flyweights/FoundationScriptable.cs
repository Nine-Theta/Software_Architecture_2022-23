using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FoundationScriptable", menuName = "ScriptableObjects/Foundation")]
public class FoundationScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "FoundationTile";

    [SerializeField]
    private GameObject _foundationModel;

    public int Cost = 0;

    public string GetName { get { return _name; } }

    public int CreationCost { get { return Cost; } }

    public GameObject GetModel
    {
        get { return _foundationModel; }
    }
}
