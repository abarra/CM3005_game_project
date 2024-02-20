using System;

public interface IInputController
{
    void SubscribeInputEvents(Action<InputData> action);
    void UnsubscribeInputEvents(Action<InputData> action);
}