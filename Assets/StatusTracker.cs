using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class StatusTracker : Singleton<StatusTracker>
{
    public int totalSections = 12;
    public float TotalTimeInSeconds = 300.0f; //in seconds
    public float timeLeft = 0;
    //private float startTime = 0;
    public float timeUnitPerDash;

    private bool paused = false;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI strokeCountText;

    void Start()
    {
        timeLeft = TotalTimeInSeconds;
        timeUnitPerDash = TotalTimeInSeconds / totalSections;
    }
    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.ACTIVE || paused)
            return;
        timeLeft -= Time.deltaTime;
        string timeLeftVisual = "";
        Debug.Log(timeLeft);
        int dashesRemaining = (int)(timeLeft / timeUnitPerDash);

        if (dashesRemaining < (totalSections/4))
        {
            timerText.color = Color.red;
        }
        else if (dashesRemaining < (totalSections / 2))
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = Color.green;
        }

        for (int i = 0; i < dashesRemaining; i++)
        {
            timeLeftVisual += "O";
        }
        timerText.text = "Oxygen: " + timeLeftVisual;
        if (timeLeft < 0)
        {
            //Trigger somthing
            GameManager.Instance.GameOver();
        }

        strokeCountText.text = "Stroke Number: " + GameManager.Instance.strokeCount;
    }

    public void Reset()
    {
        paused = false;
        timeLeft = TotalTimeInSeconds;
    }

    internal void Pause()
    {
        paused = true;
    }

    //yuck
    /*    public void Pause()
        {
            timeLeft = float.PositiveInfinity;
        }*/
}