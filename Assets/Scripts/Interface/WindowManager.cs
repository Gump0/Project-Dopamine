using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages window behavior to simulate 
// how a window manager functions within an operating system
public class WindowManager : MonoBehaviour
{
    private List<GameObject> windowList = new List<GameObject>();
    protected int index {get; private set;}

    public void UpdateFocusedWindow(GameObject clickedWindow){
        if(!windowList.Contains(clickedWindow)) return;
        else Debug.Log($"{clickedWindow} not found within the windowList");

        windowList.Remove(clickedWindow);
        windowList.Insert(0, clickedWindow);
    }

    public void SpawnWindow(GameObject newWindow){
        windowList.Add(newWindow);
        Instantiate(newWindow);
    }
}
