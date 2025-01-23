using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToAlphabetSprites : MonoBehaviour
{
    public string essay;
    public Sprite[] alphabetSprites;                                // We still need to store a collection of sprites :(
    public Transform[] LetterTransforms;                            // stores the transforms for the word objects
    private int charIndex = 0, essayCharCount;
    [SerializeField] private char currentChar;                                       // the character the player is currently hovering
    private Dictionary<char, Sprite> charToSpriteMap;               // Dictionary to store the mapping of chars to sprites

    [SerializeField] private LetterObject[] loadedLetters = new LetterObject[12];    // Stores every instance of LetterObject currently loaded
    
    struct LetterObject{
        public Sprite sprite { get; set; }                          // assinged alphabet sprite
        public char character { get; set; }                         // assigned char
        public bool isTyped { get; set; }                           // checks if the letter has been typed
        
        public LetterObject(Sprite sprite, char character, bool isTyped)
        {                                                           // constructor
            this.sprite = sprite;
            this.character = character;
            this.isTyped = isTyped;
        }
    }

    private void InitDictionary()
    {
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
            { 'Z', alphabetSprites[27] }
        };
    }

    void Start() {
        essayCharCount = essay.Length;
        InitDictionary();

        UpdateLetterObjects();
    }

    void Update() {
        if(Input.anyKeyDown) { // check for keyboard inputs
            string input = Input.inputString;

             if (string.IsNullOrEmpty(input))
                return;
            currentChar = input[0]; // store input as char
            if(currentChar == essay[charIndex]) { // if pressed char equals current char on-screen...
                charIndex++;
                UpdateLetterObjects();
            }
        }
    }

    void UpdateLetterObjects() {
        for(int i = 0; i < LetterTransforms.Length - 1; i++) {
            CharToSpriteGameObject(essay[charIndex + i], i);
        }
    }

    GameObject CharToSpriteGameObject(char c, int index)
    {
        if (charToSpriteMap.TryGetValue(c, out Sprite sprite)) {
            LetterObject letter = new LetterObject(sprite, c, false);

            GameObject letterGameObject = new GameObject($"Letter_{c}");
            SpriteRenderer sr = letterGameObject.AddComponent<SpriteRenderer>();
            sr.sprite = letter.sprite;

            letterGameObject.transform.localScale = new Vector3(5,5,5);
            letterGameObject.transform.position = LetterTransforms[index].position;

            return letterGameObject;
        }
        else {
            Debug.LogWarning($"Character '{c}' not found in dictionary.");
            return null;
        }
    }
}
