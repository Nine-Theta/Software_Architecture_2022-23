using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractSpawnFactory<T> : MonoBehaviour where T : ScriptableObject
{
    public AbstractShell<T> ShellObject;

    public abstract void Spawn(Vector3 pPosition, Quaternion pRotation);

    protected virtual AbstractShell<T> SpawnObject(Vector3 pPosition, Quaternion pRotation)
    {
        GameObject newObject = Instantiate(ShellObject.gameObject, pPosition, pRotation);
        return newObject.GetComponent<AbstractShell<T>>();
    }

}
