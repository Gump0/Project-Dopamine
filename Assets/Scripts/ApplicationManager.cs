using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is in charge of managing what scenes are loaded throughout run-time
// Or "Applications"
public class ApplicationManager : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    // Method in charge with opening the corresponding application
    // Any button in system tray will pass the name of its corresponding scene through this method.
    public void OpenApplication(string appName) {
        SceneManager.LoadScene(appName);
    }
}
