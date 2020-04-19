using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { ACTIVE,PAUSED,GAMEOVER,DIALOG} // will add levels here too later
    public int strokeCount {get; protected set; } = 0; // How many strokes it took to get to the hole
    public int aggregatedScore { get; set; } = 0; // Strokes devided by how much time you took;

    public int currentLevel = 0;

    public GameState gameState = GameState.DIALOG;

    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject dialogScreen;

    public GameObject goldFish;
    public GameObject gameStatus;

    public List<GameObject> levels;

    float timeSinceLastCall;

    public bool isGameOver { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        //initial Level Loaded
       loadLevel(5);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCall += Time.deltaTime;
    }
    public void AddStrokes(int strokes)
    {
        strokeCount += strokes;
    }

    void CalculateScore()
    {
        if (strokeCount <= 0)
            return;
        aggregatedScore = (int)(StatusTracker.Instance.timeLeft / strokeCount) * 10;
        if (aggregatedScore <= 1)
            aggregatedScore = 10;
        Debug.Log(aggregatedScore);
    }
    internal void GameOver()
    {
        
        if (GameManager.Instance.gameState == GameManager.GameState.PAUSED)
            return;
        CalculateScore();
        gameState = GameState.PAUSED;

        StartCoroutine(showGameOverCanvas(3));

        goldFish.GetComponent<FishController>().Die();
    }

    internal void Win()
    {
        
        if (GameManager.Instance.gameState == GameManager.GameState.PAUSED)
            return;

        if (timeSinceLastCall < 10)
            return;

        timeSinceLastCall = 0;
        CalculateScore();
        gameState = GameState.PAUSED;

        StartCoroutine(showWinCanvas(0));

        //goldFish.GetComponent<FishController>().Die();
    }

    private IEnumerator showGameOverCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverController>().generateWittyGameOverPun();
    }
    private IEnumerator showWinCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        winScreen.SetActive(true);
    }
    public void loadLevel(int level, bool isRestart=false)
    {
        currentLevel = level;

        if (!isRestart)
        {
            gameState = GameState.DIALOG;
            dialogScreen.SetActive(true);
            dialogScreen.GetComponent<DialogController>().ReplayDialog();
        }
        gameState = GameState.ACTIVE;

        goldFish.SetActive(true);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Camera.main.GetComponent<CameraFollower>().transistionSpeed = 25.0f;
        goldFish.transform.position = levels[level].GetComponent<CourseController>().StartPoint.transform.position;
        goldFish.GetComponent<FishController>().Reset();
        StatusTracker.Instance.Reset();
        strokeCount = 0;
    }

    public void LoadNextHole()
    {
        StartCoroutine(delayLoad(1.0f));
    }

    public void ReloadCourse()
    {
        currentLevel = 0;
        loadLevel(currentLevel,true);
    }

    public void ReloadHole()
    {
        loadLevel(currentLevel,true);
        goldFish.SetActive(true);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    private IEnumerator delayLoad(float time)
    {
        currentLevel++;
        loadLevel(currentLevel);
        goldFish.SetActive(true);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        yield return new WaitForSeconds(time);

    }

    public CourseController getActiveCourseController()
    {
        return levels[currentLevel].GetComponent<CourseController>();
    }
}
