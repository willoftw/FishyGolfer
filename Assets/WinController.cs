using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public TextMeshProUGUI punText;
    public TextMeshProUGUI scoreText;

    public GameObject nextHoleButton;
    public GameObject retryButton;
    public bool isFinal = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinal)
        {
            scoreText.text = "Final Score:" + GameManager.Instance.finalScore;
            punText.text = "Thanks for playing, this fish is now safe!";
            nextHoleButton.SetActive(false);
            retryButton.SetActive(false);
        }
        else
        {
            scoreText.text = "Score:" + GameManager.Instance.aggregatedScore;
        }
        
    }

    public void LoadNextHole()
    {
        Debug.Log("Loading Next Hole");
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

    public void BackToMainMenu()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(0);
    }
}
