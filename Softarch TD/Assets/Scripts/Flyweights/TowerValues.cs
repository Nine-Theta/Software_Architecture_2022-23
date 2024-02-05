using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerValues
{
    [Tooltip("How much damage the turret does per shot")]
    public float Damage;

    [Tooltip("The Range of the turret in Unity Units (meters)")]
    public float Range;

    [Tooltip("How often the turret shoots per Second")]
    public float Cooldown;

    [Tooltip("How much credits it costs to either build or upgrade the tower")]
    public int Cost;

    [Tooltip("How The tower looks at this stage")]
    public GameObject TowerModel;


    public TowerValues(float pDamage, float pRange, float pCooldown, int pCost)
    {
        Damage = pDamage;
        Range = pRange;
        Cooldown = pCooldown;
        Cost = pCost;
    }

    public TowerValues(TowerValues pOriginal) : this(pOriginal.Damage, pOriginal.Range, pOriginal.Cooldown, pOriginal.Cost) { }
}
