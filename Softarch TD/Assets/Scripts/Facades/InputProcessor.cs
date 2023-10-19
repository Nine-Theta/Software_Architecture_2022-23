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
    private AbstractProcessorState _currentState;

    public AbstractInstanceFactory ConstructionFactory { get; private set; }

    [SerializeField]
    private NavigationManager _navManager;

    [SerializeField]
    private DevEnemySpawner _devSpawner;

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

    public void StartLevel()
    {
        //Should be offloaded to a level manager
        if (_navManager.IsLayoutValid())
        {
            _devSpawner.AllowSpawn = true;
        }
        else
        {
            Debug.Log("Level Layout not Valid, gotta give the enemies a chance, ya know");
        }
    }
}
