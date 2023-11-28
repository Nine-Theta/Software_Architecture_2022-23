using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(PlayerInput))]
public class PlayerControls : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField]
    private InputProcessor _processor;

    public Vector2 MouseSensitivity;

    [SerializeField]
    private Vector3 _moveInput = Vector3.zero;

    [SerializeField]
    private float _zoomInput = 0;

    [Range(10, 50f)]
    public float MoveSensitivity;
    [Range(0, 5f)]
    public float ScrollSensitivity;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnMove(InputValue pValue)
    {
        Vector3 vec = pValue.Get<Vector3>();

        _moveInput = Vector3.zero;

        _moveInput.x = vec.x * MoveSensitivity;
        _moveInput.y = vec.y * MoveSensitivity;
        _moveInput.z = vec.z * MoveSensitivity;
    }

    private void OnFreeLook(InputValue pValue)
    {
        Vector2 vec = pValue.Get<Vector2>();

        transform.Rotate(0, vec.x * MouseSensitivity.x, 0);
        _playerInput.camera.transform.Rotate(-vec.y * MouseSensitivity.y, 0, 0);
        
    }

    private void OnClick(InputValue pValue)
    {
        _processor.ProccessButtonClick(pValue.Get<Vector2>());
        //Debug.Log("OnClick: " + pValue.Get());
    }

    private void OnZoom(InputValue pValue )
    {
        _zoomInput = pValue.Get<float>() * ScrollSensitivity;
    }

    private void Update()
    {
        Vector3 movement = (_moveInput.x * transform.right) + new Vector3(0,_moveInput.y,0) + (_moveInput.z * transform.forward) + (_zoomInput * _playerInput.camera.transform.forward);

        transform.position += movement * Time.deltaTime;
    }
}
