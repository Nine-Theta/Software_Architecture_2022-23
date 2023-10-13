using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPublisher
{
    //makes me feel like I'm developing a subscription-based live-service

    public delegate void EventSubscriber();

    private List<EventSubscriber> _subscribers;

    public void Subscribe(EventSubscriber pSub)//EventSubscriber<T> pSub)
    {
        _subscribers.Add(pSub);
    }

    public void Unsubscribe(EventSubscriber pSub)
    {
        _subscribers.Remove(pSub);
    }

    public void Publish()
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Invoke();
        }
    }
}

public class EventPublisher<T>
{
    //makes me feel like I'm developing a generic subscription-based live-service

    public delegate void EventSubscriber(T pArgument);

    private List<EventSubscriber> _subscribers;

    public void Subscribe(EventSubscriber pSub)
    {
        _subscribers.Add(pSub);
    }

    public void Unsubscribe(EventSubscriber pSub)
    {
        _subscribers.Remove(pSub);
    }

    public void Publish(T pArgument)
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Invoke(pArgument);
        }
    }
}
