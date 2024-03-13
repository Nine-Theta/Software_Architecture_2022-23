using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script to be attached to any GameObject to make it look at the specified camera.
/// </summary>
public class SimpleBillboardScript : MonoBehaviour
{
    [SerializeField]
    private Camera _targetCamera;

    private void Start()
    {
        if (_targetCamera == null)
            _targetCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_targetCamera.transform);
    }
}
