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
            { 'A', alphabetSprites[2] },
            { 'B', alphabetSprites[3] },
            { 'C', alphabetSprites[4] },
            { 'D', alphabetSprites[5] },
            { 'E', alphabetSprites[6] },
            { 'F', alphabetSprites[7] },
            { 'G', alphabetSprites[8] },
            { 'H', alphabetSprites[9] },
            { 'I', alphabetSprites[10] },
            { 'J', alphabetSprites[11] },
            { 'K', alphabetSprites[12] },
            { 'L', alphabetSprites[13] },
            { 'M', alphabetSprites[14] },
            { 'N', alphabetSprites[15] },
            { 'O', alphabetSprites[16] },
            { 'P', alphabetSprites[17] },
            { 'Q', alphabetSprites[18] },
            { 'R', alphabetSprites[19] },
            { 'S', alphabetSprites[20] },
            { 'T', alphabetSprites[21] },
            { 'U', alphabetSprites[22] },
            { 'V', alphabetSprites[23] },
            { 'W', alphabetSprites[24] },
            { 'X', alphabetSprites[25] },
            { 'Y', alphabetSprites[26] },
            { 'Z', alphabetSprites[27] },
            { '!', alphabetSprites[28] },
            { '?', alphabetSprites[29] },
            { '\'', alphabetSprites[30] },
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

        elapsedTime += Time.deltaTime;
        CheckIfTimeExceeded();

        if(timerUI != null) timerUI.UpdateTimerUI(savedTime - elapsedTime);
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
