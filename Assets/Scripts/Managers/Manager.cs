using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI oldTimeText;

    private Manager instance;
    private TimeManager timeManager;
    private ScoreManager scoreManager;

    private LapTime lapTime = new LapTime(0,0,0);
    private int lapCount;
    private int score;
    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        lapCount = 0;
        ChangeText();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Lap();
        }
    }
    public void Lap()
    {
        lapTime = timeManager.SaveTime();
        score += scoreManager.CalculateScore(lapTime, lapCount);
        lapCount++;
        ChangeText();
    }

    private void ChangeText()
    {
        lapText.text = ("Lap :" + lapCount.ToString());
        scoreText.text = ("Score : " + score.ToString());
        oldTimeText.text = string.Format("{0:00}:{1:00}:{2:000}", lapTime.minutes, lapTime.seconds, lapTime.milliSeconds);
    }

    protected virtual void OnEnable()
    {
        if (!instance || instance == this) instance = this as Manager;
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
