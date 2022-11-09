using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private float m_time = 0;
    private bool m_timeIsRunning = false;
    private TextMeshProUGUI m_timeText;

    private void Start()
    {
        m_timeText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (m_timeIsRunning)
        {
            if(m_time < 10000)
            {
                m_time += Time.deltaTime;
            }
            else
            {
                m_time = 10000;
                m_timeIsRunning = false;
            }
        }
        DisplayTime(m_time);
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        m_timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void StartTime()
    {
        m_time = 0;
        m_timeIsRunning = true;
    }
}

