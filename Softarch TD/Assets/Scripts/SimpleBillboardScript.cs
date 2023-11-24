using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
