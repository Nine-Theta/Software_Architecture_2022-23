using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class TowerModelController : MonoBehaviour
{
    [SerializeField]
    private Transform _towerPivot;

    [SerializeField]
    private Animator _gunAnimator;

    private TowerObject _towerObject;

    private Transform _targetTransform;

    private bool _hasTarget = false;
    private bool _useAnimation = false;

    public void Initialize(TowerObject pTower)
    {
        _towerObject = pTower;
        _towerObject.TargetAcquired.Subscribe(OnTargetAcquired);

        _useAnimation = (_gunAnimator != null);
    }

    public void OnDrawGizmos()
    {
        Handles.DrawLine(_towerPivot.position, _towerPivot.position + (_towerPivot.forward * 4), 2f);
    }

    private void Update()
    {
        if (_hasTarget)
        {
            if(_targetTransform != null && Vector3.SqrMagnitude(_towerObject.transform.position - _targetTransform.position) < _towerObject.RuntimeValues.Range * _towerObject.RuntimeValues.Range)
            {
                _towerPivot.LookAt(_targetTransform);
            }
            else
            {
                _hasTarget = false;
                if (_useAnimation)
                    _gunAnimator.SetBool("GunIsFiring", false);
            }
        }
    }

    private void OnTargetAcquired(Transform pTarget)
    {
        _targetTransform = pTarget;
        _hasTarget= true;

        if (_useAnimation)
        {
            _gunAnimator.SetBool("GunIsFiring", true);
            _gunAnimator.speed = _towerObject.GetCurrentValues().Cooldown;
        }
    }
}
