using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPlayerControls : MonoBehaviour
{
    public Camera cam;

    public Vector3 MouseOld;

    public Vector2 Sensitivity;

    public Vector3 movement = Vector3.zero;

    public float MoveSensitivity;
    public float ScrollSensitivity;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 delta = Input.mousePosition - MouseOld;
            transform.Rotate(0, delta.x * Sensitivity.x, 0);
            cam.transform.Rotate(-delta.y * Sensitivity.y, 0, 0);
        }

        if (Input.GetKey(KeyCode.W)) movement += transform.forward;
        if (Input.GetKey(KeyCode.A)) movement -= transform.right;
        if (Input.GetKey(KeyCode.S)) movement -= transform.forward;
        if (Input.GetKey(KeyCode.D)) movement += transform.right;
        movement += Input.mouseScrollDelta.y * ScrollSensitivity * cam.transform.forward;

        transform.position += movement * MoveSensitivity;
        movement = Vector3.zero;

        MouseOld = Input.mousePosition;
    }
}
