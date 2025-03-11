using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DesktopTimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public Image hourGlassIcon;

    float elapsedTime;

    private void Update() { // handle animation timer
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 3) {
            StartCoroutine(HourGlassShake());
            elapsedTime = 0;
        }
    }
    public void UpdateTimerUI(float timePassed) { // Called by external classes to update UI elements
        timerText.text = TimerText(timePassed);
    }

    private string TimerText(float timePassed) { // returns correct string time according to time passed compared to remaining time
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);

        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private IEnumerator HourGlassShake() { // handles shake animation
    Quaternion originalRotation = hourGlassIcon.transform.rotation;
    float shakeAmount = 5f;

    float shakeDuration = 0.5f;

    float elapsedTime = 0f;

    while (elapsedTime < shakeDuration)
    {
        float timeFactor = Mathf.PingPong(elapsedTime * 10f, 1f);

        hourGlassIcon.transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, Mathf.Lerp(-shakeAmount, shakeAmount, timeFactor));

        elapsedTime += Time.deltaTime;
        yield return null;
    }
    hourGlassIcon.transform.rotation = originalRotation;
    }
}
