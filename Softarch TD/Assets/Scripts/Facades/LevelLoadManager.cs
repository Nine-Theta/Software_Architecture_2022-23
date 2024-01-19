using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    [SerializeField]
    private SceneAsset TestLevel;

    [SerializeField, ReadOnly]
    private string _nextScene = "";

    [SerializeField, ReadOnly]
    private string _previousScene = "";

    private Commander _loadLevelCommander = new Commander();

    private bool _isLoading = false;


    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    [Button]
    public void LoadLevelTest()
    {
        LoadNewLevel(TestLevel);
    }

    public void LoadNewLevel(Scene pScene)
    {
        LoadNewLevel(pScene.name);
    }

    public void LoadNewLevel(SceneAsset pScene)
    {
        LoadNewLevel(pScene.name);
    }

    public void LoadNewLevel(string pScene)
    {
        if (_isLoading)
        {

            return;
        }

        _isLoading = true;

        if (_loadLevelCommander.HasCommands())
        {
            _loadLevelCommander.UndoCommand();
            _nextScene = pScene;
        }
        else
        {
            _loadLevelCommander.ExecuteCommand(new LoadSceneCommand(pScene, LoadSceneMode.Additive));            
            _previousScene = pScene;
        }
    }

    private void OnSceneUnloaded(Scene pScene)
    {
        if (pScene.name == _previousScene)
        {
            _loadLevelCommander.ExecuteCommand(new LoadSceneCommand(_nextScene, LoadSceneMode.Additive));
        }
    }

    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        if (pScene.name == _nextScene)
        {
            SceneManager.SetActiveScene(pScene);
            _previousScene = _nextScene;
            _isLoading = false;
        }
    }
}
