using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerModelController : MonoBehaviour
{
    [SerializeField]
    private Transform _towerPivot;

    private TowerObject _towerObject;

    private Transform _targetTransform;

    private bool _hasTarget = false;

    public void Initialize(TowerObject pTower)
    {
        _towerObject = pTower;
        _towerObject.TargetAcquired.Subscribe(OnTargetAcquired);
        _towerObject.TargetLost.Subscribe(OnTargetLost);
    }

    public void OnDrawGizmos()
    {
        Handles.DrawLine(_towerPivot.position, _towerPivot.position + (_towerPivot.forward * 4), 2f);
    }

    private void Update()
    {
        if (_hasTarget)
        {
            _towerPivot.LookAt(_targetTransform);
        }
    }

    private void OnTargetAcquired(Transform pTarget)
    {
        _targetTransform = pTarget;
        _hasTarget= true;
    }

    private void OnTargetLost()
    {
        _hasTarget = false;
    }
}
