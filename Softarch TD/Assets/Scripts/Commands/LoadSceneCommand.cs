using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <see cref="I_Command"/> that loads/unloads a specified scene
/// </summary>
public class LoadSceneCommand : I_Command
{
    private string _sceneBackup;

    private LoadSceneMode _modeBackup;

    public LoadSceneCommand(string pSceneName, LoadSceneMode pMode)
    { 
        _sceneBackup = pSceneName;    
        _modeBackup = pMode;
    }

    public bool Execute()
    {
        SceneManager.LoadScene(_sceneBackup, _modeBackup);
        return true;
    }

    public void Undo()
    {
        SceneManager.UnloadSceneAsync(_sceneBackup);
    }
}
