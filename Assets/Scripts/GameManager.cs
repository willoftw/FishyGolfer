using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int strokeCount {get; protected set; } = 0; // How many strokes it took to get to the hole
    int arregatedScore { get; set; } = 0; // Strokes devided by how much time you took;

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
}
