using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerScriptable", menuName = "ScriptableObjects/Foundation")]
public class FoundationScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "FoundationTile";

    [SerializeField]
    private GameObject _containerObject;

    public int Cost = 0;

    public string GetName { get { return _name; } }

    public GameObject GetContainerObject { get { return _containerObject; } }
}
