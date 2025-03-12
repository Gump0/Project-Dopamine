using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// SNAKE GAME CODE!
// Courtesy to Zach for getting this working
public class SnakeManager : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data
    public static event Action OnPlayerDeath;
    
    public int highScore = 0;
    Food food;

    public PlayAudio playAudioScript;

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        highScore = gameState.snakeHighScore;
        food = GameObject.FindWithTag("Food").GetComponent<Food>();
        food.UpdateHighScore();
    }

    public void PlayerDeath() {
        Debug.Log("Player has died!");
        OnPlayerDeath?.Invoke();

        playAudioScript.playClip();
    }

    public void SaveSnakeData() { // called on game close (just like I coded for the Essay)
        gameState.snakeHighScore = highScore;
        SaveSystem.Save(gameState);
    }
}
