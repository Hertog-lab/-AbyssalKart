using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_timeText;

    private TimeManager instance;

    private float m_time = 0;
    private bool m_timeIsRunning = false;
    private LapTime lapTime;

    private float minutes, seconds, milliSeconds;
    private void Start()
    {
        m_timeIsRunning = true;
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
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        milliSeconds = (timeToDisplay % 1) * 1000;
        m_timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void StartTime()
    {
        m_time = 0;
        m_timeIsRunning = true;
    }

    public LapTime SaveTime()
    {
        lapTime = new LapTime(minutes, seconds, milliSeconds);
        m_time = 0;
        milliSeconds = 0;
        seconds = 0;
        minutes = 0;
        return lapTime;
    }

    protected virtual void OnEnable()
    {
        if (!instance || instance == this) instance = this as TimeManager;
        else
        {
            Destroy(this);
            Debug.LogError("Instance already exists!");
        }
    }
    protected virtual void OnDisable()
    {
        if (instance == this) instance = null;
    }
    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize()
    {
        OnEnable();
    }
}

public class LapTime
{
    public float minutes;
    public float seconds;
    public float milliSeconds;
    
    public LapTime(float t_minutes, float t_seconds, float t_milliseconds)
    {
        minutes = t_minutes;
        seconds = t_seconds;
        milliSeconds = t_milliseconds;
    }
}

