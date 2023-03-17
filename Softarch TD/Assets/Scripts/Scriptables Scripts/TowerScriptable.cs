using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerScriptable", menuName = "ScriptableObjects/Tower")]
public class TowerScriptable : ScriptableObject
{
    public string TowerType = "todo"; //type of attack
    public string AttackStrategy = "todo"; //which enemy to attack
    public string Name = "tower";

    public float Damage = 0f;
    public float Cooldown = 0.1f;
    public float Range = 1;

    public int Cost = 1;

    public List<string> Debuffs = new List<string>();

}
