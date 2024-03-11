using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A singleton that is unique per Unity Scene. It contains all the scene-specific information that scene-inpedendant script rely on.
/// </summary>
/// <remarks>Contains references to the scene's <see cref="BaseManager"/>, <see cref="EnemySpawnManager"/>, <see cref="NavMeshSurface"/>, and <see cref="NavMeshAgent"/> required for testing</remarks>
public class SceneSettings : MonoBehaviour
{
    private static SceneSettings _instance;
    public static SceneSettings Instance { get { return _instance; } }

    [SerializeField]
    private int _credits;

    [SerializeField]
    private BaseManager _sceneBase;

    [SerializeField]
    private EnemySpawnManager _sceneSpawnManager;

    [SerializeField]
    private NavMeshSurface _sceneNavMeshSurface;

    [SerializeField]
    private NavMeshAgent _sceneNavigiationTester;


    private void Awake()
    {
        //Ensuring the newest scenesettings instance will always be used
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
        }

        _instance = this;
    }

    public int GetSceneCredits()
    {
        return _credits;
    }

    public BaseManager GetSceneBase()
    {
        return _sceneBase;
    }

    public EnemySpawnManager GetSceneSpawnManager()
    {
        return _sceneSpawnManager;
    }

    public NavMeshSurface GetSceneNavMeshSurface()
    {
        return _sceneNavMeshSurface;
    }

    public NavMeshAgent GetSceneNavigiationTester()
    {
        return _sceneNavigiationTester;
    }
}
