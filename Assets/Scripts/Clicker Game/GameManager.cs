using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data

    public Text ClicksTotalText;
    public Text AutoModText;
    //public Text Upgrade1AmountText;
    //public Text Upgrade2AmountText;
    //public Text Upgrade3AmountText;

    public int TotalClicks, autoClickModifier = 0;
    public int TotalClicksModifier = 0;

    public int autoClicksPerSecond;
    public int minimumClicksToUnlockUpgrade;
    public int minimumClicksToUnlockUpgrade2;
    public int minimumClicksToUnlockUpgrade3;
    public int minimumClicksToUnlockMedal;
    public int Upgrade1Amount;
    public int Upgrade2Amount;
    public int Upgrade3Amount;

    public GameObject Medal;
    bool hasMedal = false;

    bool hasUpgrade3;
    float elapsedTime;

    public PlayAudio playAudioScriptForUp1;
    public PlayAudio playAudioScriptForUp2;
    public PlayAudio playAudioScriptForUp3;
    public PlayAudio playAudioScriptForMedal;

    // LOCALTIMER.CS BOILERPLATE CODE (rip locattimer.cs)
    [SerializeField] float savedTime;            // time stored
    [SerializeField] float elapsedSaveTime;          // time spent on scene

    DesktopTimerUI timerUI;                     // store timer ui class and use only in desktop

    public void Start()
    {
        gameState = SaveSystem.Load(); // Load saved data
        hasMedal = gameState.hasMedal;
        TotalClicks = gameState.totalClickerScore;
        autoClickModifier = gameState.clickerUpgrade;
        
        Medal.SetActive(false);
        if(GameObject.Find("Timer") != null) {  // check if timer object exists
            timerUI = GameObject.Find("Timer").GetComponent<DesktopTimerUI>();
        }
        savedTime = gameState.maxGameTime;

        ClicksTotalText.text = TotalClicks.ToString();
        AutoModText.text = autoClickModifier.ToString();
    }
    public void AddClicks()
    {
        TotalClicks++;
        ClicksTotalText.text = TotalClicks.ToString("0");
        if (hasUpgrade3)
        {
            for (int i = 0; i < TotalClicksModifier; i++)
            {
                TotalClicks++;
                ClicksTotalText.text = TotalClicks.ToString();
            }
        }
    }

    public void AutoClickUpgrade()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade)
        {
            autoClickModifier++;
            TotalClicks -= minimumClicksToUnlockUpgrade;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade1Amount++;
            //Upgrade1AmountText.text = Upgrade1Amount.ToString();

            playAudioScriptForUp1.playClip();
        }
    }

    public void AutoClickUpgrade2()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade2)
        {
            autoClickModifier += 3;
            TotalClicks -= minimumClicksToUnlockUpgrade2;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade2Amount++;
            //Upgrade2AmountText.text = Upgrade2Amount.ToString();

            playAudioScriptForUp2.playClip();
        }
    }

    public void ClickUpgrade3()
    {
        if (TotalClicks >= minimumClicksToUnlockUpgrade3)
        {
            hasUpgrade3 = true;
            TotalClicksModifier++;
            TotalClicks -= minimumClicksToUnlockUpgrade3;
            ClicksTotalText.text = TotalClicks.ToString();
            Upgrade3Amount++;
            //Upgrade3AmountText.text = Upgrade3Amount.ToString();

            playAudioScriptForUp3.playClip();
        }
    }

    public void BuyMedal()
    {
        if (TotalClicks >= minimumClicksToUnlockMedal)
        {
            Medal.SetActive(true);
            TotalClicks -= minimumClicksToUnlockMedal;
            ClicksTotalText.text = TotalClicks.ToString();
            hasMedal = true;

            playAudioScriptForMedal.playClip();
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < 2f) return;
        elapsedTime = 0;
        if (autoClickModifier == 0) return;
        for (int i = 0; i < autoClickModifier; i++)
        {
            TotalClicks++;
        }
        ClicksTotalText.text = TotalClicks.ToString();
        AutoModText.text = autoClickModifier.ToString();

        elapsedSaveTime += Time.deltaTime;
        CheckIfTimeExceeded();

        if(timerUI != null) timerUI.UpdateTimerUI(savedTime - elapsedSaveTime);
    }

    public void SaveGame() {
        gameState.hasMedal = hasMedal;
        gameState.totalClickerScore = TotalClicks;
        gameState.clickerUpgrade = autoClickModifier;
        SaveTimeData();
        SaveSystem.Save(gameState);
    }

    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //
    // LOCALTIMER.CS BOILERPLATE METHODS
    // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= //

    public void SaveTimeData() { // ALWAYS SAVE TIME DATA BEFORE SWITCHING SCENES
        gameState.maxGameTime = savedTime - elapsedSaveTime;
        SaveSystem.Save(gameState);
    }

    private void CheckIfTimeExceeded() { // called whenever elapsedTime + savedTime > time limit
        if (savedTime - elapsedSaveTime > 0) return;
        ApplicationManager appMan = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();

        appMan.OpenApplication("ENDSCENE"); // end the game
    }
}

