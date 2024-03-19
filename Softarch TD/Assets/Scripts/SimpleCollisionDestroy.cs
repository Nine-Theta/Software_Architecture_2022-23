using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

/// <summary>
/// Simple script that destroys the gameObject it's attached to when it collides with something
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SimpleCollisionDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
