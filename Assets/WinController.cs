using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinController : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + GameManager.Instance.arregatedScore;
    }

    public void LoadNextHole()
    {
        GameManager.Instance.LoadNextHole();
    }

    public void replayCourse()
    {
        GameManager.Instance.ReloadCourse();
    }

    public void replayHole()
    {
        GameManager.Instance.ReloadHole();
    }
}
