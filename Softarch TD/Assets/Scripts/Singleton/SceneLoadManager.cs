using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A singleton that loads and unloads scenes through the use of <see cref="LoadSceneCommand"/>s
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager _instance;
    public static SceneLoadManager Instance { get { return _instance; } }


    [SerializeField]
    private SceneAsset TestLevel;

    [SerializeField]
    private SceneAsset _titleSceneAsset;

    [SerializeField]
    private SceneAsset _overlaySceneAsset;

    [SerializeField]
    private string _nextScene;


    private LoadSceneCommand _loadTitleCommand;
    private LoadSceneCommand _loadOverlayCommand;

    private LoadSceneCommand _loadLevelCommand = null;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _loadTitleCommand = new LoadSceneCommand(_titleSceneAsset.name, LoadSceneMode.Single);
        _loadOverlayCommand = new LoadSceneCommand(_overlaySceneAsset.name, LoadSceneMode.Additive);

        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    [Button]
    public void LoadLevelTest()
    {
        LoadLevel(TestLevel);
    }

    public void LoadTitleScreen()
    {
        _loadLevelCommand = null;
        _loadTitleCommand.Execute();
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ReloadLevel()
    {
        _loadLevelCommand.Execute();
    }

    public void LoadLevel(Scene pScene) { LoadLevel(pScene.name); }
    public void LoadLevel(SceneAsset pScene) { LoadLevel(pScene.name); }
    public void LoadLevel(string pScene)
    {
        _nextScene = pScene;

        _loadLevelCommand = new LoadSceneCommand(pScene, LoadSceneMode.Single);
        _loadLevelCommand.Execute();
    }

    private void OnSceneUnloaded(Scene pScene)
    {

    }

    private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
    {
        if (pScene == SceneManager.GetSceneByName(_nextScene))
        {
            SceneManager.SetActiveScene(pScene);
            _loadOverlayCommand.Execute();
        }
    }
}
