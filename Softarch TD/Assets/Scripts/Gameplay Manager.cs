using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int _credits;
    public int Credits
    {
        get { return _credits; }
        set
        {
            _credits = value;
            CreditsUpdated.Publish(_credits);
        }
    }

    public EventPublisher<int> CreditsUpdated = new EventPublisher<int>();

    private void Start()
    {
        CreditsUpdated.Publish(_credits);
    }
}
