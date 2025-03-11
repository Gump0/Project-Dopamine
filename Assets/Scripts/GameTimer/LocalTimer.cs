// Class in-charge with managing the games timer
// reading and writing data to JSON is used to store game time
using UnityEngine;

public class LocalTimer : MonoBehaviour
{
    public GameState gamestate; // Gamestate ref

    float savedTime;            // time stored
    float elapsedTime;          // time spent on scene

    [Header("MAXIMUM GAME TIME")]
    public float maxGameTime = 600f;   // detirmines the maximum duration of play

    public void SaveTimeData() { // ALWAYS SAVE TIME DATA BEFORE SWITCHING SCENES
        gamestate.gameTime = elapsedTime + savedTime;
        SaveSystem.Save(gamestate);
    }

    private void CheckIfTimeExceeded() { // called whenever elapsedTime + savedTime > time limit
        if (savedTime + elapsedTime < maxGameTime) return;
        ApplicationManager appMan = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();

        appMan.OpenApplication("ENDSCENE"); // end the game
    }

    void Start() {
        gamestate = SaveSystem.Load();
        savedTime = gamestate.gameTime;
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        CheckIfTimeExceeded();
    }
}
