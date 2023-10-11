using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPublisher<T>
{
    //makes me feel like I'm developing a subscription-based live-service

    private List<EventSubscriber<T>> _subscribers;

    public void Subscribe()//EventSubscriber<T> pSub)
    {
        //_subscribers.Add(pSub);
        UnityEvent e = null;
        e.AddListener(Publish);


    }

    public void Unsubscribe(EventSubscriber<T> pSub)
    {
        _subscribers.Remove(pSub);
    }

    public void Publish()//T pArgument)
    {
        for(int i = 0; i < _subscribers.Count; i++)
        {
            //_subscribers[i].DoThing(this, pArgument);
        }
    }
}

public delegate void EventSubscriber
{


    public delegate T DoThing(EventPublisher<T> pPublisher, T pArgument);
}
