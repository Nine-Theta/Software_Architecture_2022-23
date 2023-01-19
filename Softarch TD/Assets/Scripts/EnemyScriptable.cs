using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScriptable", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public string MovemenStrategy = "todo";

    public string Name = "Bob";

    [SerializeField, Tooltip("Total health the enemy has")]
    private float _baseHealth = 10.0f;

    [SerializeField, Tooltip("Unmodified movementspeed of the Enemy")]
    private float _baseMovementSpeed = 1.0f;

    [SerializeField, Tooltip("A flat reduction to any damage the enemy takes")]
    private float _baseDefense = 0.0f;

    [SerializeField, Range(-1.0f, 1.0f), Tooltip("A percentage reduction to the damage the enemy takes, after defense has been deducted")]
    private float _baseResistance = 0.0f;

    [SerializeField, Tooltip("How much damage the enemy does to the Objective")]
    private float _baseAttackDamage = 1.0f;

    public Mesh Mesh;


    [Header("Runtime Values")]
    public float Health = 10.0f;
    public float MovementSpeed = 1.0f;
    public float Defense = 0.0f;
    public float Resistance = 0.0f;
    public float AttackDamage = 1.0f;


    private void OnEnable()
    {
        Health = _baseHealth;
        MovementSpeed = _baseMovementSpeed;
        Defense = _baseDefense;
        Resistance= _baseResistance;
        AttackDamage = _baseAttackDamage;
    }

    public float TakeDamage(float pDamage)
    {
        Health = (pDamage - Defense) * (1-Resistance);
        return Health;
    }
}
