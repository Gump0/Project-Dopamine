using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is in charge of managing what scenes are loaded throughout run-time
// Or "Applications"
public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GameObject appOpenPrefab; // prefab reference to window that simulates loading
    [Range(0.05f, 1.25f)] [SerializeField] private float duration = 0.5f; // duration of animation

    bool isLoading = false; // keeps track if window is already animating

    // Method in charge with opening the corresponding application
    // Any button in system tray will pass the name of its corresponding scene through this method.
    public void OpenApplication(string appName) {
        SceneManager.LoadScene(appName);
    }
    // Special case method to be called when a application is run
    // From the Desktop scene, including the window open logic.
    public void AnimateWindowOpen(string appName) {
        if(isLoading) return; // this is included to prevent mutliple loading animations at once
        isLoading = true;
        GameObject openApp = Instantiate(appOpenPrefab);
        StartCoroutine(AnimateWindow(openApp, appName));
    }

    // Method that grows window before launching the application.
    private IEnumerator AnimateWindow(GameObject growWindow, string appName) {
        if(growWindow == null){
            Debug.LogWarning("Application Manager Class : growWindow is null");
            yield break;
        }
        RectTransform rect = growWindow.GetComponent<RectTransform>();

        Vector3 initialSize = Vector3.zero;
        Vector3 finalScale = new Vector3(25, 25, 1);

        float elapsedTime = 0;
        while(elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            float bounce = Mathf.Sin(t * Mathf.PI * (0.5f + 2 * t)) * (1f - t) + t;
            rect.localScale = Vector3.LerpUnclamped(initialSize, finalScale, bounce);
            yield return null;
        }
        rect.localScale = finalScale;

        // Launch the application after the animation completes
        OpenApplication(appName);
    }
}
