using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A simple script that was used for the startscene in an earlier stage of the project.
/// </summary>
public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    private SceneAsset StartLevel;

    [SerializeField]
    private SceneAsset OverlayLevel;

    public void LoadStartLevel()
    {
        SceneManager.LoadScene(StartLevel.name);
        SceneManager.LoadScene(OverlayLevel.name, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
