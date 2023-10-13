using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PublishEventCommand : I_Command
{
    private EventPublisher _receiver;
    public PublishEventCommand(EventPublisher pPublisher)
    {
        _receiver = pPublisher;
    }

    public bool Execute()
    {
        _receiver.Publish();
        return false;
    }

    public void Undo()
    {
        
    }
}
