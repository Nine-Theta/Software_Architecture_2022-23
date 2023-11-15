using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerModelController : MonoBehaviour
{
    [SerializeField]
    private Transform _towerPivot;

    private TowerObject _towerObject;

    public void Initialize(TowerObject pTower)
    {
        _towerObject = pTower;
    }
}
