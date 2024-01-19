using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

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
