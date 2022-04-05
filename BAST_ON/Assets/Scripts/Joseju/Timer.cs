using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float StartingTime = 30;
    [SerializeField] private Text timeText;

    private float _timeRemaining;

    private bool isTimerRunning = false;




    public void updateTimer(bool value)
    {
        isTimerRunning = value;
        timeText.enabled = value;
    }

    private void UpdateUI(float timeValue)
    {
        int minutes = Mathf.FloorToInt(_timeRemaining / 60);
        int seconds = Mathf.FloorToInt(_timeRemaining % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //meter efectos de explosiones?
    }

    private void HandleTimerEnd()
    {
        //explotacionar todo
    }

    void Start()
    {
        _timeRemaining = StartingTime;
        timeText.enabled = false;
    }

    void Update()
    {
        if (isTimerRunning)
        {
             if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                UpdateUI(_timeRemaining);
                //actualizar UI
            }
            else
            {
                HandleTimerEnd();
                _timeRemaining = 0;
                isTimerRunning = false;
            }
        }
        
    }
}
