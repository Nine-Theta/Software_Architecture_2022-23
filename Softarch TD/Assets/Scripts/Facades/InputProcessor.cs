using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private AbstractProcessorState _currentState;

    [SerializeField]
    public GameplayManager GetGameplayManager { get { return _gameplayManager; } }
    public UIManager GetUIManager { get { return _uiManager; } }

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
        _gameplayManager.ConstructionFactory = pFactory;
    }

    public void StartLevel()
    {
        _gameplayManager.StartNextWave();
    }
}
