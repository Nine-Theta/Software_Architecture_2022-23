using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that determines if a Unity UI button should be interactable based on a specified <see cref="TowerScriptable"/>'s creation cost;
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonCreditCheckScript : MonoBehaviour
{
    [SerializeField]
    private GameplayManager _gameplayManager;

    private Button _button;

    [SerializeField]
    private TowerScriptable _tower;

    public void Awake()
    {
        _button = GetComponent<Button>();
        _gameplayManager.CreditsUpdated.Subscribe(CheckCredit);
    }

    public void OnEnable()
    {
        CheckCredit();
    }

    public void CheckCredit(int pCredit = 0)
    {
        if(_tower.CreationCost > _gameplayManager.Credits)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}
