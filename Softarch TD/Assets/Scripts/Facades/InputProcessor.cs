using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputProcessor : MonoBehaviour
{
    [SerializeField]
    public TowerScriptable _selectedTower = null;

    [SerializeField]
    private AbstractProcessorState _currentState;

    public TowerFactory TowerFactory;

    public Commander TowerBuildCommander = new Commander();

    public TextMeshProUGUI CreditUI;

    [SerializeField]
    private int _credits;
    public int Credits
    {
        get { return _credits; }
        set
        {
            _credits = value;
            CreditUI.text = _credits.ToString();
        }
    }



    public void Start()
    {
        _currentState.SetContext(this);
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
