using UnityEngine;

/// <summary>
/// A simple script to be attached to any GameObject to make it look at the specified transform.
/// </summary>
public class SimpleBillboardScript : MonoBehaviour
{
    [SerializeField, Tooltip("If left null, will use the main camera by default")]
    private Transform _targetObject;

    [SerializeField, Tooltip("Reverses the direction the billboard looks towards, used for world UI")]
    private bool _invert;

    private void Start()
    {
        if (_targetObject == null)
            _targetObject = Camera.main.transform;
    }

    private void Update()
    {
        if (!_invert)
            transform.LookAt(_targetObject.transform.position);
        else
            transform.LookAt(transform.position + (transform.position - _targetObject.transform.position));
    }
}
