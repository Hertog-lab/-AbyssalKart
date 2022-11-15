using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int startPoints = 10;
    [SerializeField] private int secondsPerPoints = 10;
    
    private ScoreManager instance;
    private LapTime oldLaptime;
    private float oldTimeToCalculate;

    public int CalculateScore(LapTime lapTime, int lapCount)
    {
        if(lapCount > 0)
        {
            oldTimeToCalculate = oldLaptime.milliSeconds + (oldLaptime.seconds * 1000) + ((oldLaptime.minutes * 60) * 1000);
            float minutes = lapTime.minutes;
            float seconds = lapTime.seconds;
            float milliSeconds = lapTime.milliSeconds;
            float timeToCalculate = milliSeconds + (seconds * 1000) + ((minutes * 60) * 1000);

            float calculatedtime = oldTimeToCalculate % timeToCalculate;
            if(calculatedtime > 0)
            {
                float result = calculatedtime / (secondsPerPoints * 1000);
                result = Mathf.RoundToInt(result);
                return (int)result;
            }
            else if(calculatedtime < 0)
            {
                float result = calculatedtime / (secondsPerPoints * 1000);
                result = Mathf.RoundToInt(result);
                if(result <= 0)
                {
                    return 0;
                }
                else
                {
                    return (int)result;
                }
            }
            else
            {
                return startPoints;
            }
        }
        else
        {
            oldLaptime = lapTime;
            return startPoints;
        }
    }


    protected virtual void OnEnable()
    {
        if (!instance || instance == this) instance = this as ScoreManager;
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
