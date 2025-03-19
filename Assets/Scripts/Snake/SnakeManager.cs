using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// SNAKE GAME CODE!
// Courtesy to Zach for getting this working
public class SnakeManager : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data
    // SNAKE CODE
    public static event Action OnPlayerDeath;
    public int highScore = 0;

    Food food;
    // LOCALTIMER.CS BOILERPLATE CODE (rip localtimer.cs)
    [SerializeField] float savedTime;            // time stored
    [SerializeField] float elapsedTime;          // time spent on scene

    DesktopTimerUI timerUI;                     // store timer ui class and use only in desktop

    public PlayAudio playAudioScript;

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        highScore = gameState.snakeHighScore;
        food = GameObject.FindWithTag("Food").GetComponent<Food>();
        food.UpdateHighScore();

        if(GameObject.Find("Timer") != null) {  // check if timer object exists
            timerUI = GameObject.Find("Timer").GetComponent<DesktopTimerUI>();
        }
        savedTime = gameState.maxGameTime;
    }

    public void PlayerDeath() {
        Debug.Log("Player has died!");
        OnPlayerDeath?.Invoke();

        playAudioScript.playClip();
    }

    public void SaveSnakeData() { // called on game close (just like I coded for the Essay)
        gameState.snakeHighScore = highScore;
        SaveTimeData();
        SaveSystem.Save(gameState);
    }
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //
    // LOCALTIMER.CS BOILERPLATE METHODS
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //

    public void SaveTimeData() { // ALWAYS SAVE TIME DATA BEFORE SWITCHING SCENES
        gameState.maxGameTime = savedTime - elapsedTime;
        SaveSystem.Save(gameState);
    }

    private void CheckIfTimeExceeded() { // called whenever elapsedTime + savedTime > time limit
        if (savedTime - elapsedTime > 0) return;
        ApplicationManager appMan = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();

        appMan.OpenApplication("ENDSCENE"); // end the game
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        CheckIfTimeExceeded();

        if(timerUI != null) timerUI.UpdateTimerUI(savedTime - elapsedTime);
    }
}
