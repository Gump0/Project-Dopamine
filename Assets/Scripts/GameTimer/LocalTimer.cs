// Class in-charge with managing the games timer
// reading and writing data to JSON is used to store game time
using UnityEngine;

public class LocalTimer : MonoBehaviour
{
    public GameState gamestate; // Gamestate ref

    [SerializeField] float savedTime;            // time stored
    [SerializeField] float elapsedTime;          // time spent on scene

    DesktopTimerUI timerUI;                     // store timer ui class and use only in desktop

    public void SaveTimeData() { // ALWAYS SAVE TIME DATA BEFORE SWITCHING SCENES
        gamestate.maxGameTime = savedTime - elapsedTime;
        SaveSystem.Save(gamestate);
    }

    private void CheckIfTimeExceeded() { // called whenever elapsedTime + savedTime > time limit
        if (savedTime - elapsedTime > 0) return;
        ApplicationManager appMan = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();

        appMan.OpenApplication("ENDSCENE"); // end the game
    }

    void Start() {
        if(GameObject.Find("Timer") != null) {  // check if timer object exists
            timerUI = GameObject.Find("Timer").GetComponent<DesktopTimerUI>();
        }

        gamestate = SaveSystem.Load();
        savedTime = gamestate.maxGameTime;
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        CheckIfTimeExceeded();

        if(timerUI != null) timerUI.UpdateTimerUI(savedTime - elapsedTime);
    }
}
