using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    [Description("[0] is base values, everything else is upgrades")]
    public List<TowerValues> TowerRankValues;

    public string GetName { get { return _name; } }
    public int CreationCost { get { return TowerRankValues[0].Cost; } }

    //To be aplied to Enemy
    public List<DebuffScriptable> Debuffs = new List<DebuffScriptable>();

    public GameObject GetModel
    {
        get { return _towerModel; }
    }
}
