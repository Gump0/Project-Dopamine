using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInfo : MonoBehaviour
{   
    public GameObject windowedRef;
    public bool isFullscreened = false;
    private WindowManager windowManager;
    private WindowFunctions windowFunctions;
    private Vector3 prevSize, prevPos;

    void Start() {
        if(windowManager == null)
            windowManager = GameObject.Find("WindowManager").GetComponent<WindowManager>();
        if(windowFunctions == null)
            windowFunctions = GameObject.Find("WindowManager").GetComponent<WindowFunctions>();
    }

    public void MinimizeOrFullescreen(GameObject minimizedWindowVers) { // METHOD TO BE CALLED FROM MINIMIZE TOGGLE BUTTON
    windowedRef = minimizedWindowVers;
        switch(isFullscreened) {
            case true:
                MinimizeWindow();
                Debug.Log("Window fullscreen status is " + isFullscreened);
            break;

            case false:
                MaximizeWindow();
                Debug.Log("Window fullscreen status is " + isFullscreened);
            break;
        }
    }

    private void MinimizeWindow() { // FULLSCREEN GAMEOBJECT  >> MINIMIZED GAMEOBJECT  
        windowedRef.transform.localScale = prevSize;
        windowedRef.transform.position = prevPos;

        isFullscreened = false;
    }

    private void MaximizeWindow() { // MINIMIZED GAMEOBJECT >> FULLSCREEN GAMEOBJECT
        windowManager.windowList.Remove(windowedRef);
        windowManager.windowList.Insert(0, windowedRef);
        windowManager.UpdateListedSortingOrders();

        AdjustToFullScreen(windowedRef);
        isFullscreened = true;
    }

    public virtual void AdjustToFullScreen(GameObject minWindow) { // Adjust fullscreen window prefab and its components to fit all screens
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        
        // Bottom-left corner world coordinates
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

        // Top-right corner world coordinates
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, mainCamera.nearClipPlane));

        // World space dimensions
        float worldWidth = topRight.x - bottomLeft.x;
        float worldHeight = topRight.y - bottomLeft.y;
        
        prevSize = minWindow.transform.localScale; // store prevoius window size
        Vector3 fullscreenScale = new Vector3(worldWidth, worldHeight, 0);
        minWindow.transform.localScale = fullscreenScale;
        
        prevPos = minWindow.transform.position;
        minWindow.transform.position = Vector3.zero;
    }

    public virtual void SaveData() { // SAVE WINDOW DATA UNDER CERTAIN USE CASES
        
    }
}
