using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FoundationScriptable", menuName = "ScriptableObjects/Foundation")]
public class FoundationScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "FoundationTile";

    [SerializeField]
    private GameObject _containerObject;

    public int Cost = 0;

    public string GetName { get { return _name; } }

    public GameObject GetContainerObject
    {
        get
        {
            if (_containerObject.GetComponent<FoundationObject>() == null)
            {
                Debug.LogError("Container Object for :" + this + " Is NULL or does not contain the proper script. Go fix it");
                return null;
            }
            else
                return _containerObject;
        }
    }
}
