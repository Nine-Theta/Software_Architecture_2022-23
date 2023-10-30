using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerScriptable", menuName = "ScriptableObjects/Tower")]
public class TowerScriptable : ScriptableObject, I_Containable
{
    [SerializeField]
    private string _name = "tower";

    [SerializeField]
    private GameObject _towerModel;

    public string TowerType = "todo"; //type of attack
    public AbstractAttackStrategy AttackStrategy; //which enemy to attack

    public TowerValues BaseValues;

    public List<TowerValues> UpgradeValues;

    public string GetName { get { return _name; } }
    public int CreationCost { get { return BaseValues.Cost; } }

    //To be aplied to Enemy
    public List<string> Debuffs = new List<string>();

    public GameObject GetModel
    {
        get { return _towerModel; }
    }
}
