using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFunctions : MonoBehaviour
{
    private WindowManager windowManager;
    void Start() {
        if(windowManager == null) 
            windowManager = GetComponent<WindowManager>();
    }

    public void SpawnWindow(GameObject newWindow){
        windowManager.windowList.Add(newWindow);
        Instantiate(newWindow);
    }

    public void CloseWindow(GameObject closingButton) {
        windowManager.RemoveClosedWindowData(closingButton);
        GameObject closingWindow = GrabElementWindowParent(closingButton);
        windowManager.windowList.Remove(closingWindow);
        Destroy(closingWindow);
    }

    public void MinimizeToTaskBar(GameObject m2tbButton) { 
        // Method will evenutally handle storing to taskbar
        // For now this method simply hides the window unsing SetActive()
        GameObject m2tbWindow = GrabElementWindowParent(m2tbButton);
        m2tbWindow.SetActive(false); 
    }

    public void MinMaxWindow(GameObject mmButton) {
        GameObject mmButtonWindow = GrabElementWindowParent(mmButton);
        WindowInfo mmWindowWindowInfo = mmButtonWindow.GetComponent<WindowInfo>();
        mmWindowWindowInfo.MinimizeOrFullescreen(mmButtonWindow); // execute method under WindowInfo.cs
    }

    private GameObject GrabElementWindowParent(GameObject childObj) {
        Transform current = childObj.transform;
        while (current.parent != null) {
            if(current.gameObject.tag == "Window") // stop at window and not object that stores every window 😭
                return current.gameObject;
            current = current.parent;
        }
        return current.gameObject;
    }
}
