using System.Collections.Generic;
using UnityEngine;

// Script in charge with handling essay logic
// checking if correct inputs are used,
// and displaying the correct characters the player needs to type
public class StringToAlphabetSprites : MonoBehaviour
{
    public GameState gameState; // gamestate reference to store data

    public string essay;
    public Sprite[] alphabetSprites;                                // We still need to store a collection of sprites :(
    public Transform[] letterTransforms;                            // stores the transforms for the word objects
    public int charIndex = 0, essayCharCount;
    [SerializeField] private char currentChar;                       // the character the player is currently hovering
    private Dictionary<char, Sprite> charToSpriteMap;               // Dictionary to store the mapping of chars to sprites

    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private GameObject[] letterObjects = new GameObject[13];          // Letter Objects

    // LOCALTIMER.CS BOILERPLATE CODE (rip locattimer.cs)
    [SerializeField] float savedTime;            // time stored
    [SerializeField] float elapsedTime;          // time spent on scene

    DesktopTimerUI timerUI;                     // store timer ui class and use only in desktop

    private void InitDictionary() {
        charToSpriteMap = new Dictionary<char, Sprite>
        {   // represents 26 english characters + comma & peroid (28 total)
            { ',', alphabetSprites[0] },
            { '.', alphabetSprites[1] },
            { 'A', alphabetSprites[2] }, { 'a', alphabetSprites[3] },
            { 'B', alphabetSprites[4] }, { 'b', alphabetSprites[5] },
            { 'C', alphabetSprites[6] }, { 'c', alphabetSprites[7] },
            { 'D', alphabetSprites[8] }, { 'd', alphabetSprites[9] },
            { 'E', alphabetSprites[10] }, { 'e', alphabetSprites[11] },
            { 'F', alphabetSprites[12] }, { 'f', alphabetSprites[13] },
            { 'G', alphabetSprites[14] }, { 'g', alphabetSprites[15] },
            { 'H', alphabetSprites[16] }, { 'h', alphabetSprites[17] },
            { 'I', alphabetSprites[18] }, { 'i', alphabetSprites[19] },
            { 'J', alphabetSprites[20] }, { 'j', alphabetSprites[21] },
            { 'K', alphabetSprites[22] }, { 'k', alphabetSprites[23] },
            { 'L', alphabetSprites[24] }, { 'l', alphabetSprites[25] },
            { 'M', alphabetSprites[26] }, { 'm', alphabetSprites[27] },
            { 'N', alphabetSprites[28] }, { 'n', alphabetSprites[29] },
            { 'O', alphabetSprites[30] }, { 'o', alphabetSprites[31] },
            { 'P', alphabetSprites[32] }, { 'p', alphabetSprites[33] },
            { 'Q', alphabetSprites[34] }, { 'q', alphabetSprites[35] },
            { 'R', alphabetSprites[36] }, { 'r', alphabetSprites[37] },
            { 'S', alphabetSprites[38] }, { 's', alphabetSprites[39] },
            { 'T', alphabetSprites[40] }, { 't', alphabetSprites[41] },
            { 'U', alphabetSprites[42] }, { 'u', alphabetSprites[43] },
            { 'V', alphabetSprites[44] }, { 'v', alphabetSprites[45] },
            { 'W', alphabetSprites[46] }, { 'w', alphabetSprites[47] },
            { 'X', alphabetSprites[48] }, { 'x', alphabetSprites[49] },
            { 'Y', alphabetSprites[50] }, { 'y', alphabetSprites[51] },
            { 'Z', alphabetSprites[52] }, { 'z', alphabetSprites[53] },
            { '!', alphabetSprites[54] },
            { '?', alphabetSprites[55] },
            { '\'', alphabetSprites[56] },
            { '/', alphabetSprites[56] },
            { '*', alphabetSprites[56] },
            { ' ', null }
        };
    }

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        charIndex = gameState.essayCharIndex; // Restore essay progress

        essayCharCount = essay.Length;
        InitDictionary();
        for(int i = 0; i < letterTransforms.Length; i++) {
            CharToSpriteGameObject(i, currentChar);
            UpdateEachLetterGameObj(i, essay[i + charIndex]);
        }

        if(GameObject.Find("Timer") != null) {  // check if timer object exists
            timerUI = GameObject.Find("Timer").GetComponent<DesktopTimerUI>();
        }
        savedTime = gameState.maxGameTime;
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        CheckIfTimeExceeded();

        if(timerUI != null) timerUI.UpdateTimerUI(savedTime - elapsedTime);

        if(charIndex >= essayCharCount) {
            gameState.essayComplete = true;
            return; // check if end of essay is reached.
        } 

        if(Input.anyKeyDown) { // check for keyboard inputs
            string input = Input.inputString;

             if (string.IsNullOrEmpty(input))
                return;
            currentChar = input[0]; // store input as char
            //Debug.Log(essay[charIndex]);
            if(currentChar == essay[charIndex]) { // if pressed char equals current char on-screen...
                charIndex++;
                UpdateLetterObjects();
            } else {
                Debug.LogWarning("Incorrect Key Has Been Inputted");
                // IncorrectKeyPress(some index)
            }
        }
    }

    void UpdateLetterObjects() { // Called to update every letter in list
        for(int i = 0; i < letterObjects.Length; i++) {
            if((i + charIndex) < essayCharCount) { // check if charIndex exceeds what characters are left
            UpdateEachLetterGameObj(i, essay[i + charIndex]);
            } else {
                HideLetterObject(i);
            }
        }
    }

    void UpdateEachLetterGameObj(int index, char c) { // Update each letter to correspond with inputted essay text
        Transform tr = letterTransforms[index];
        LetterData data = letterObjects[index].GetComponent<LetterData>();

        if(charIndex >= 0 && charIndex < essayCharCount) { // Check if index exists in string
            if (charToSpriteMap.TryGetValue(c, out Sprite charSprite)) {
                data.UpdateSprite(charSprite);
            }
        } 
        // else {
        //     HideLetterObject(index);
        // }
    }

    void CharToSpriteGameObject(int index, char c) { // Generate every letter object ( EXECUTED ONLY ONCE )
        if (charToSpriteMap.TryGetValue(c, out Sprite sprite)) {
            Transform tr = letterTransforms[index];
            GameObject letterGameObject = Instantiate(letterPrefab, tr);
            //Debug.Log($"Instantiated object for character: {c}");
            letterGameObject.name = $"Letter_{c}";
            LetterData data = letterGameObject.GetComponent<LetterData>();
            data.UpdateSprite(sprite);
            data.character = c;
            
            letterObjects[index] = letterGameObject;
        }
        else {
            Debug.LogWarning($"Character '{c}' not found in dictionary.");
        }
    }

    private void IncorrectKeyPress(int index) { // called when the INCORRECT key is inputted

    }

    private void HideLetterObject(int index) {  // called in use cases when letter object must be blank
        Sprite nullSprite = null;               // usually when a space is displayed or if the index is out of bounds
        LetterData data = letterObjects[index].GetComponent<LetterData>();
        data.UpdateSprite(nullSprite);
    }

    public void SaveEssayData() {
        gameState.essayCharIndex = charIndex;
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
}
