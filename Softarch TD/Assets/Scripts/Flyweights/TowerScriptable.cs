using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerScriptable", menuName = "ScriptableObjects/Tower")]
public class TowerScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "tower";

    [SerializeField]
    private GameObject _containerObject;

    public string TowerType = "todo"; //type of attack
    public AbstractAttackStrategy AttackStrategy; //which enemy to attack

    public float Damage = 0f;
    public float Cooldown = 0.1f;
    public float Range = 1;

    public int Cost = 1;


    public List<string> Debuffs = new List<string>();

    public GameObject GetContainerObject
    {
        get
        {
            if (_containerObject.GetComponent<AbstractContainerObject<TowerScriptable>>() == null)
            {
                Debug.LogError("Container Object for :"+this+" Is NULL or does not contain the proper script. Go fix it");
                return null;
            }
            else
                return _containerObject;
        }
    }

    public string GetName
    {
        get { return _name; }
    }
}
