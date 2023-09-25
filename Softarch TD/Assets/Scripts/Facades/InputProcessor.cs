using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    [SerializeField]
    public TowerScriptable _selectedTower = null;

    [SerializeField]
    private AbstractProcessorState _currentState;

    public TowerBuildScript TowerBuilder;

    public Commander TowerBuildCommander = new Commander();


    public void Start()
    {
        this._currentState = new DefaultState(this);
    }

    public void ChangeState(AbstractProcessorState pState)
    {
        _currentState = pState;
        _currentState.SetContext(this);
    }

    public void ProccessButtonClick(Vector2 pMousePos)
    {
        Debug.Log("process");

        _currentState.ProccessButtonClick(pMousePos);
    }

    public void SelectTower(TowerScriptable pTower)
    {
        _selectedTower = pTower;
    }

    
}
