using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages window behavior to simulate 
// how a window manager functions within an operating system
public class WindowManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> windowList = new List<GameObject>();
    protected int index {get; private set;}

    private void Start() {
        // find every ungrouped window prior to game runtime
        // and put it in list
        GameObject[] unGroupedWindows = GameObject.FindGameObjectsWithTag("Window");

        foreach(GameObject wn in unGroupedWindows){
            windowList.Add(wn);
        }
        UpdateListedSortingOrders(); // make sure all windows are sorted right away! :D
    }

    public void UpdateFocusedWindow(GameObject clickedWindow) { // On window click
        if(windowList.Contains(clickedWindow))
            Debug.LogWarning("contains clicked window");
            windowList.Remove(clickedWindow);
        
        windowList.Insert(0, clickedWindow);
        UpdateListedSortingOrders();
    }
    public void UpdateListedSortingOrders() {
        // check list hiearchy and update each window
        // and window component to the correct sorting order :3
        int maxNumberOfChildren = 4; // variable that determines how deep of a sorting order a parent window needs
        int elementIterations = 1;
        for(int i = 0; i < windowList.Count; i++) {
            SpriteRenderer winSr = windowList[i].GetComponent<SpriteRenderer>();
            winSr.sortingOrder = i * -maxNumberOfChildren; // make sorting order a negative multiple of # of elements
            int prevWindowSortingNumber = winSr.sortingOrder; // store sorting order of previous window

            foreach(Transform winElementTransform in windowList[i].transform) {
                // grabs every window element
                // iterate through all of them and make sure they fit within
                // windowList max#ofchilren multiples
                SpriteRenderer winElementSr = winElementTransform.gameObject.GetComponent<SpriteRenderer>();
                winElementSr.sortingOrder = prevWindowSortingNumber + elementIterations;
                if(elementIterations < maxNumberOfChildren - 1)
                    elementIterations++;
            }
        }
    }

    private void UpdateFocusedWindowComponents(GameObject clickedWindow, int parentWindowSortingOrder) { // add
        // Update each window components sorting order
    }

    public void SpawnWindow(GameObject newWindow) {
        windowList.Add(newWindow);
        Instantiate(newWindow);
    }
}
