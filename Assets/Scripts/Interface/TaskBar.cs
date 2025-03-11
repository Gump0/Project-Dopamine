using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    [SerializeField] private GameObject taskBarTray;
    [SerializeField] private GameObject audioTaskBarTray;
    bool sysTrayActive = false;
    bool audioTrayActive = false;

    // Method in charge with opening system tray when called
    public void OpenTaskTray() {
        sysTrayActive = !sysTrayActive;
        taskBarTray.SetActive(sysTrayActive);
    }

    // Method in charge with opening audio tray when called
    public void OpenAudioTaskTray()
    {
        audioTrayActive = !audioTrayActive;
        audioTaskBarTray.SetActive(audioTrayActive);
    }
}