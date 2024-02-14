﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private static TimerManager _instance;
    public static TimerManager Instance { get { return _instance; } }

    // Timer variables
    public float startTime = 10f; // Initial time in seconds
    private float currentTime;
    private bool isTimerRunning = false;

    public static event Action OnTimerEnd;
    public static event Action OnTimerStart;
    public static event Action OnTimerPaused;

    public TextMeshProUGUI timerText;

    private void Awake()
    {
        // Singleton pattern implementation
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // Initialize the timer
        ResetTimer();
    }

    private void Update()
    {
        // Update the timer only if it's running
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            // Check if the timer has reached zero
            if (currentTime <= 0f)
            {
                EndGame();
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        OnTimerStart?.Invoke();
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
        OnTimerPaused?.Invoke();
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        isTimerRunning = false;
        UpdateTimerDisplay();
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        // Update the UI to display the current time
        timerText.text = currentTime.ToString("0");
    }

    private void EndGame()
    {
        ResetTimer();
        OnTimerEnd?.Invoke();
    }
}
