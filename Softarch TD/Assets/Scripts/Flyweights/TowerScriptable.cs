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

    public float Damage = 0f;
    public float Cooldown = 0.1f;
    public float Range = 1;

    public int Cost = 1;

    public string GetName { get { return _name; } }
    public int CreationCost { get { return Cost; } }


    public List<string> Debuffs = new List<string>();

    public GameObject GetModel
    {
        get { return _towerModel; }
    }
}
