using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages window behavior to simulate 
// how a window manager functions within an operating system
public class WindowManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> windowList = new List<GameObject>();
    [SerializeField] private SpriteRenderer prevWindow;
    protected int index {get; private set;}

    private void Start(){
        // find every ungrouped window prior to game runtime
        // and put it in list
        GameObject[] unGroupedWindows = GameObject.FindGameObjectsWithTag("Window");

        foreach(GameObject wn in unGroupedWindows){
            windowList.Add(wn);
        }
    }

    public void UpdateFocusedWindow(GameObject clickedWindow){
        if(prevWindow != null) prevWindow.sortingOrder = 0;
        if(!windowList.Contains(clickedWindow)){
            Debug.Log($"{clickedWindow} not found within the windowList"); 
            return;
        } 

        windowList.Remove(clickedWindow);
        windowList.Insert(0, clickedWindow);
        
        SpriteRenderer clickedWindowSR = windowList[0].GetComponent<SpriteRenderer>();
        clickedWindowSR.sortingOrder = 5;
        prevWindow = clickedWindowSR;
    }

    public void SpawnWindow(GameObject newWindow){
        windowList.Add(newWindow);
        Instantiate(newWindow);
    }
}
