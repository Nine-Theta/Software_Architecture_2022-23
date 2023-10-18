using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyTarget;

    public Vector3 GetEnemyTarget
    {
        get { return _enemyTarget.transform.position; }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Has entered the Forbidden Zone(tm)");
    }
}
