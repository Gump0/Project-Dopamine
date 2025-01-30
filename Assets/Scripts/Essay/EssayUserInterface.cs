using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script in charge with handling logic related to the essays UI
// progress bar, WPM counter, and any other UI elements..
public class EssayUserInterface : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Image typeIndicator;
    private StringToAlphabetSprites stringToAlphabetSprites;
    private float progress;
    
    // Indicator variables
    float elapsedTime;
    bool isVisable = false;

    void Start() { // StringToAlphabetSprites & EssayUserInterface Will always be on the same GameObject
        stringToAlphabetSprites = GetComponent<StringToAlphabetSprites>();
        if(stringToAlphabetSprites == null) Debug.LogError("EssayUserInterface CLASS : stringToAlphabetSprites IS NULL");
    }

    void Update() {
        UpdateProgressBar();
        UpdateStatusText();
        TypeIndicatorLoop();
    }

    void UpdateProgressBar() { // Updates the progress bar for player input
        progress = (float)stringToAlphabetSprites.charIndex / (float)stringToAlphabetSprites.essayCharCount;
        progressBar.value = progress;

        if(progress == 1) {
            progressBar.gameObject.SetActive(false);
        }
    }

    void UpdateStatusText() { // Updates the tmp asset based off player input
        float percentText =  progress * 100;
        percentText = Mathf.Round(percentText);
        string displayText = percentText.ToString();
        statusText.text = displayText;
 
        if(progress == 100)
             statusText.text = "CONGRAGULATIONS! YOU HAVE COMPLETED THE ESSAY";
    }

    void TypeIndicatorLoop() { // looping underline animation designed to serve as a indicator
        elapsedTime += Time.deltaTime;
        if(elapsedTime < 0.5f) return;
        isVisable = !isVisable;
        typeIndicator.gameObject.SetActive(isVisable);
        elapsedTime = 0;
    }
}