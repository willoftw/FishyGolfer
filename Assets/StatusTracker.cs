using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StatusTracker : MonoBehaviour
{
    public int totalSections = 12;
    public float timeLeftInSeconds = 300.0f; //in seconds
    private float startTime = 0;
    public float timeUnitPerDash;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI strokeCountText;

    void Start()
    {
        startTime = timeLeftInSeconds;
        timeUnitPerDash = startTime / totalSections;
    }
    void Update()
    {
        timeLeftInSeconds -= Time.deltaTime;
        string timeLeftVisual = "";
        Debug.Log(timeLeftInSeconds / timeUnitPerDash);
        int dashesRemaining = (int)(timeLeftInSeconds / timeUnitPerDash);

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
            timeLeftVisual += "-";
        }
        timerText.text = timeLeftVisual;
        if (timeLeftInSeconds < 0)
        {
            //Trigger somthing
            GameManager.Instance.isGameOver = true;
        }

        strokeCountText.text = "Stroke Number: " + GameManager.Instance.strokeCount;
    }
}