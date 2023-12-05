using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    private SceneAsset StartLevel;

    public void LoadStartLevel()
    {
        SceneManager.LoadScene(StartLevel.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
