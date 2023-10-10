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

    public AbstractInstanceFactory ConstructionFactory;

    public Commander ConstructionCommander = new Commander();

    public TextMeshProUGUI CreditUI;

    [SerializeField]
    private int _credits;   //TODO: Fix credit cost, _selectedTower is not really used anymore
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
        _currentState.ProccessButtonClick(pMousePos);
    }

    public void ChangeFactory(AbstractInstanceFactory pFactory)
    {
        ConstructionFactory = pFactory;
    }
}
