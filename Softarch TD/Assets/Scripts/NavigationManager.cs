using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    public NavMeshSurface mesh;

    [Button]
    public void RebuildMesh()
    {
        mesh.BuildNavMesh();
    }
}
