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

    public Vector3 movement = Vector3.zero;

    [Range(0, 2f)]
    public float MoveSensitivity;
    [Range(0, 3f)]
    public float ScrollSensitivity;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnMove(InputValue pValue)
    {
        Vector3 vec = pValue.Get<Vector3>();

        movement = Vector3.zero;

        movement += vec.x * MoveSensitivity * transform.right;
        movement += vec.y * MoveSensitivity * transform.forward;
        movement += vec.z * ScrollSensitivity * _playerInput.camera.transform.forward;
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
        Debug.Log("OnClick: " + pValue.Get());
    }


    private void Update()
    {
        transform.position += movement;
    }
}
