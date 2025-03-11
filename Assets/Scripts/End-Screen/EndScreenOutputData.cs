// Class that reads save data and outputs
// a final ending response based off JSON data
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenOutputData : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data

    [Header("MANAGES OUTPUTTED DATA")]
    public TextData[] dialogueData; // stores every possible end message
                                    // indexes array data
                                    // 0 = fail text
                                    // 1 = clicker text
                                    // 2 = snake text
                                    // 3 = essay text
    [Header("DISPLAYS DATA")]
    public Image snakeCheck;
    public Image essayCheck;
    public Image clickerCheck;
    public Sprite checkMark;
    public TMP_Text outputBox;

    [Header("Minimum Snake High-Score Required")]
    [Range(15, 50)]
    public int minSnakeScore;                  // snake cond

    private bool clickerGameCondition = false;  // clickergame cond
    private bool essayComplete = false;         // essay cond
    private bool snakeCondition = false;        // snake bool cond

    void Start() {
        gameState = SaveSystem.Load();
        gameState.maxGameTime = 0;
        SaveSystem.Save(gameState);
        CheckAppStats();
    }

    void CheckAppStats() {  // responsible for checking if each app condition should be checked and detirmine what the output message should be
        clickerGameCondition = gameState.hasMedal;
        if(gameState.snakeHighScore >= minSnakeScore) snakeCondition = true;
        essayComplete = gameState.essayComplete;
        
        SetComponents();
    }

    void SetComponents() {  // set each image according to boolean data and set output text
        string outputText = "";
        if (clickerGameCondition)
        {
            clickerCheck.sprite = checkMark;
            outputText = dialogueData[1].message;
            Debug.Log(clickerGameCondition);
        }
        if (snakeCondition)
        {
            snakeCheck.sprite = checkMark;
            outputText = dialogueData[2].message;
            Debug.Log(snakeCondition);
        }
        if (essayComplete)
        {
            essayCheck.sprite = checkMark;
            outputText = dialogueData[3].message;
            Debug.Log(essayComplete);
        }

        if (dialogueData == null) Debug.LogError("(dialogueData[index] == null");
        outputBox.text = outputText;
    }
}
