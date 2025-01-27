using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script in charge with handling logic related to the essays UI
// progress bar, WPM counter, and any other UI elements..
public class EssayUserInterface : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Slider progressBar;
    private StringToAlphabetSprites stringToAlphabetSprites;
    private float progress;

    void Start() { // StringToAlphabetSprites & EssayUserInterface Will always be on the same GameObject
        stringToAlphabetSprites = GetComponent<StringToAlphabetSprites>();
        if(stringToAlphabetSprites == null) Debug.LogError("EssayUserInterface CLASS : stringToAlphabetSprites IS NULL");
    }

    void Update() {
        UpdateProgressBar();
        UpdateStatusText();
    }

    void UpdateProgressBar() {
        progress = (float)stringToAlphabetSprites.charIndex / (float)stringToAlphabetSprites.essayCharCount;
        progressBar.value = progress;

        if(progress == 1) {
            progressBar.gameObject.SetActive(false);
        }
    }

    void UpdateStatusText() {
        float percentText =  progress * 100;
        percentText = Mathf.Round(percentText);
        string displayText = percentText.ToString();
        statusText.text = displayText;
 
        if(progress == 100)
             statusText.text = "CONGRAGULATIONS! YOU HAVE COMPLETED THE ESSAY";
    }
}