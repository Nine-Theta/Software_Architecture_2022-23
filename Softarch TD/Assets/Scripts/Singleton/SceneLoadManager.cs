using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private SceneAsset TestLevel;

    [SerializeField]
    private string _nextScene;

    [SerializeField]
    private string _previousScene;


    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        _previousScene = SceneManager.GetActiveScene().name;
    }

    [Button]
    public void LoadLevelTest()
    {
        LoadLevel(TestLevel);
    }

    public void LoadLevel(SceneAsset pScene)
    {
        _nextScene = pScene.name;

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    private void OnSceneUnloaded(Scene pScene)
    {
        if (pScene == SceneManager.GetSceneByName(_previousScene))
        {
            SceneManager.LoadScene(_nextScene, LoadSceneMode.Additive);
        }
    }

    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        if (pScene == SceneManager.GetSceneByName(_nextScene))
        {
            SceneManager.SetActiveScene(pScene);
            _previousScene = _nextScene;
            //_nextScene = "";
        }
    }
}
