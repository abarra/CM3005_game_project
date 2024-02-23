using System;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    private event Action<InputData> inputEvent;

    private float oldVertial = 0f;
    private float odHorizontal = 0f;

    public void SubscribeInputEvents(Action<InputData> action)
    {
        inputEvent += action;
    }

    public void UnsubscribeInputEvents(Action<InputData> action)
    {
        inputEvent -= action;
    }

    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        if ((Mathf.Abs(vertical - oldVertial) > Mathf.Epsilon  ||
            Mathf.Abs(horizontal - odHorizontal) > Mathf.Epsilon) && inputEvent != null)
        {
            oldVertial = vertical;
            odHorizontal = horizontal;
            Debug.LogWarning(vertical);
            Debug.LogWarning(horizontal);
            inputEvent(new InputData
            {
                Vertical = vertical,
                Horizontal = horizontal
            });
        }
        
    }
}