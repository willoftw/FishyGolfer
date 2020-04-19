﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI punText;
    public List<string> gameOverPuns = new List<string>()
    {
        "That couldnt have Breem easier!",
        "He swam with the fishes...",
        "I'll get the Chips on",
        "Oh my Cod, He's Dead!"
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Misleading Name, they arnt that Witty.
    public void generateWittyGameOverPun()
    {
        var random = new Random();
        punText.text = gameOverPuns[Random.Range(0, gameOverPuns.Count)];
    }
}
