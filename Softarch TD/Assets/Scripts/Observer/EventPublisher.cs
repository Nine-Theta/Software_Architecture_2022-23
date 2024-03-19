using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Implementation of the Event Pattern.
/// </summary>
public class EventPublisher
{
    //makes me feel like I'm developing a subscription-based live-service

    public delegate void EventSubscriber();

    private List<EventSubscriber> _subscribers = new List<EventSubscriber>();

    public void Subscribe(EventSubscriber pSub)//EventSubscriber<T> pSub)
    {
        _subscribers.Add(pSub);
    }

    public void Unsubscribe(EventSubscriber pSub)
    {
        if (_subscribers.Contains(pSub))
            _subscribers.Remove(pSub);
    }

    public void UnsubscribeAll()
    {
        _subscribers.Clear();
    }

    public void Publish()
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Invoke();
        }
    }
}

/// <inheritdoc cref="EventPublisher"/>
/// <typeparam name="T">Argument type subscriber methods need to adhere to</typeparam>
public class EventPublisher<T>
{
    public delegate void EventSubscriber(T pArgument);

    private List<EventSubscriber> _subscribers = new List<EventSubscriber>();

    public void Subscribe(EventSubscriber pSub)
    {
        _subscribers.Add(pSub);
    }

    public void Unsubscribe(EventSubscriber pSub)
    {
        _subscribers.Remove(pSub);
    }
    public void UnsubscribeAll()
    {
        _subscribers.Clear();
    }

    public void Publish(T pArgument)
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Invoke(pArgument);
        }
    }
}


/// <inheritdoc cref="EventPublisher"/>
/// <typeparam name="T1">First argument type subscriber methods need to adhere to.</typeparam>
/// <typeparam name="T2">Second argument type subscriber methods need to adhere to.</typeparam>
public class EventPublisher<T1,T2>
{
    public delegate void EventSubscriber(T1 pArgument1, T2 pArgument2);

    private List<EventSubscriber> _subscribers = new List<EventSubscriber>();

    public void Subscribe(EventSubscriber pSub)
    {
        _subscribers.Add(pSub);
    }

    public void Unsubscribe(EventSubscriber pSub)
    {
        _subscribers.Remove(pSub);
    }
    public void UnsubscribeAll()
    {
        _subscribers.Clear();
    }

    public void Publish(T1 pArgument1, T2 pArgument2)
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Invoke(pArgument1, pArgument2);
        }
    }
}
