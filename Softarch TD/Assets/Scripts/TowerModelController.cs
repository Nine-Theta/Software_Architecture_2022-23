using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Controls the tracking behaviour of the models used by <see cref="TowerObject"/>s, and the the spawning of visual bullets;
/// </summary>
public class TowerModelController : MonoBehaviour
{
    [SerializeField]
    private Transform _towerPivot;

    [SerializeField]
    private Animator _gunAnimator;

    [SerializeField]
    private Transform _muzzleTransform;

    [SerializeField]
    private GameObject _bulletObject;

    private TowerObject _towerObject;

    private Transform _targetTransform;

    [SerializeField]
    private bool _spawnBullet = true;

    [SerializeField]
    private bool _trackTarget = true;

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
        if (!_trackTarget)
            return;

        Handles.DrawLine(_towerPivot.position, _towerPivot.position + (_towerPivot.forward * 4), 2f);
    }

    private void Update()
    {
        if (_hasTarget)
        {
            if (_targetTransform != null && Vector3.SqrMagnitude(_towerObject.transform.position - _targetTransform.position) < (_towerObject.GetCurrentValues().Range+.5f) * (_towerObject.GetCurrentValues().Range+.5f))
            {
                if (!_trackTarget)
                    return;

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

        Debug.Log("target aqcuired! "+ pTarget.gameObject);

        if (_spawnBullet)
        {
            Instantiate(_bulletObject, _muzzleTransform.position, _muzzleTransform.rotation).GetComponent<Rigidbody>().AddForce((pTarget.position +Vector3.up - _muzzleTransform.position) *5f, ForceMode.VelocityChange);

        }

        if (_useAnimation)
        {
            _gunAnimator.SetBool("GunIsFiring", true);
            _gunAnimator.speed = _towerObject.GetCurrentValues().Cooldown;
        }
    }

    private void OnDestroy()
    {
        _towerObject.TargetAcquired.Unsubscribe(OnTargetAcquired);
    }
}
