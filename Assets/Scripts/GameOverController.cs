using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI punText;
    public TextMeshProUGUI scoreText;
    public List<string> gameOverPuns = new List<string>()
    {
        "That couldnt have Breem easier!",
        "He swam with the fishes...",
        "I'll get the Chips on.",
        "Oh my Cod, He's Dead!",
        "Im sure your just fine TUNAing.",
        "That was Eeely bad.",
        "You didn't quite free Willy",
        "Cod have done Batter"
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:" + GameManager.Instance.aggregatedScore;
    }

    //Misleading Name, they arnt that Witty.
    public void generateWittyGameOverPun()
    {
        var random = new Random();
        punText.text = gameOverPuns[Random.Range(0, gameOverPuns.Count)];
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
