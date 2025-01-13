using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    [SerializeField] private GameObject taskBarTray;
    bool sysTrayActive = false;
    // Method in charge with opening system tray when called
    public void OpenTaskTray() {
        sysTrayActive = !sysTrayActive;
        taskBarTray.SetActive(sysTrayActive);
    }
    // Method in charge with opening the corresponding application
    // Any button in system tray will pass the name of its corresponding scene through this method.
    public void OpenApplication(string appName) {

    }
}
