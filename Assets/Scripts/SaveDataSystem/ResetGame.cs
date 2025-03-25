using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data

    public void CallResetGame() {
        gameState = SaveSystem.Load();
        SaveSystem.ResetSave();
        SceneManager.LoadScene("Login Screen"); // switch to OG scene
    }
}
