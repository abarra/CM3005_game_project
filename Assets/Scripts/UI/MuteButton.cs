using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMuted;

    public bool IsMuted()
    {
        return isMuted;
    }

    [SerializeField] public  Sprite volumeOnSprite;
    [SerializeField] public  Sprite volumeOffSprite;

    [SerializeField] public Color defaultColor;
    [SerializeField] public Color mutedColor;
    [SerializeField] public Color highlightColor;

    [SerializeField] public  Image image;
    
    [SerializeField] public Toggle.ToggleEvent onChange;

    public void Start()
    {
        _transformButton();
    }

    public void SetStateWithoutNotification(bool newIsMuted)
    {
        Debug.Log($"New state of mute button. Current state: {isMuted}, new state: {newIsMuted}");
        isMuted = newIsMuted;
        _transformButton();
    }

    public void SetState(bool newIsMuted)
    {
        SetStateWithoutNotification(newIsMuted);
        onChange?.Invoke(isMuted);
    }

    private void _toggleState()
    {
        Debug.Log($"Toggle mute button. Current state: {isMuted}");
        isMuted = !isMuted;
        onChange?.Invoke(isMuted);
        _transformButton();
    }

    private void _transformButton()
    {
        if (isMuted)
        {
            image.sprite = volumeOffSprite;
            image.color = mutedColor;
        }
        else
        {
            image.sprite = volumeOnSprite;
            image.color = defaultColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _toggleState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = isMuted ? mutedColor : defaultColor;
    }
}
