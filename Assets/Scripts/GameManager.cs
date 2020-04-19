using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { ACTIVE,PAUSED,GAMEOVER} // will add levels here too later
    public int strokeCount {get; protected set; } = 0; // How many strokes it took to get to the hole
    int arregatedScore { get; set; } = 0; // Strokes devided by how much time you took;

    public int currentLevel = 0;

    public GameState gameState = GameState.ACTIVE;

    public GameObject gameOverScreen;
    public GameObject goldFish;

    public bool isGameOver { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddStrokes(int strokes)
    {
        strokeCount += strokes;
    }


    internal void GameOver()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.PAUSED)
            return;
        gameState = GameState.PAUSED;

        StartCoroutine(showGameOverCanvas(3));

        goldFish.GetComponent<FishController>().Die();
    }

    private IEnumerator showGameOverCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverController>().generateWittyGameOverPun();
    }

    public void loadLevel(int level)
    {

    }
}
