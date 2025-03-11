using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DesktopTimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public Image hourGlassIcon;

    private void Update() { // handle animation timer
        
    }
    public void UpdateTimerUI(float timePassed) { // Called by external classes to update UI elements
        timerText.text = TimerText(timePassed);
    }

    private string TimerText(float timePassed) { // returns correct string time according to time passed compared to remaining time
        return null;
    }

    private IEnumerator HourGlassShake() { // handles shake animation
        return null;
    }
}
