using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    private static SceneSettings _instance;
    public SceneSettings Instance { get { return _instance; } }

    [SerializeField]
    private BaseManager _sceneBase;

    [SerializeField]
    private EnemySpawnManager _sceneSpawnManager;


    private void Awake()
    {
        //Ensuring the newest scenesettings instance will always be used
        if(_instance != null && _instance != this)
        {
            Destroy(_instance);
        }

        _instance = this;
    }


    public BaseManager GetSceneBase()
    {
        return _sceneBase;
    }

    public EnemySpawnManager GetSceneSpawnManager()
    {
        return _sceneSpawnManager;
    }
}
