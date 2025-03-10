// Class that reads save data and outputs
// a final ending response based off JSON data
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenOutputData : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data

    [Header("MANAGES OUTPUTTED DATA")]
    public TextData[] dialogueData; // stores every possible end message
    public int index;               // indexes array data
    [Header("DISPLAYS DATA")]
    public Image snakeCheck, essayCheck, clickerCheck;
    public Image snakeIcon, essayIcon, clickerIcon;
    public TMP_Text outputBox;

    [Header("Minimum Snake High-Score Required")]
    [Range(15, 50)]
    public int minSnakeScore;                  // snake cond

    private bool clickerGameCondition = false;  // clickergame cond
    private bool essayComplete = false;         // essay cond
    private bool snakeCondition = false;        // snake bool cond

    void Start() {
        gameState = SaveSystem.Load();
        CheckAppStats();
    }

    void CheckAppStats() {  // responsible for checking if each app condition should be checked and detirmine what the output message should be
        // SET LOCAL CONDITION CHECKS BASSED OFF GAMESTATE DATA
        clickerGameCondition = gameState.hasMedal;
        if(gameState.snakeHighScore >= minSnakeScore) snakeCondition = true;
        essayComplete = gameState.essayComplete;
        
        SetComponents();
    }

    void SetComponents() {  // set each image according to boolean data and set output text

    }
}
