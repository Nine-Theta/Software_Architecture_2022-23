using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerValues
{
    [Tooltip("How much damage the turret does per shot")]
    public float Damage;

    [Tooltip("How often the turret shoots per Second")]
    public float Cooldown;

    [Tooltip("The Range of the turret in Unity Units")]
    public float Range;

    [Tooltip("How much credits it costs to either build or upgrade the tower")]
    public int Cost;


    public TowerValues(float pDamage, float pCooldown, float pRange, int pCost)
    {
        Damage = pDamage;
        Cooldown = pCooldown;
        Range = pRange;
        Cost = pCost;
    }

    public TowerValues(TowerValues pOriginal) : this(pOriginal.Damage, pOriginal.Cooldown, pOriginal.Range, pOriginal.Cost) { }
}
