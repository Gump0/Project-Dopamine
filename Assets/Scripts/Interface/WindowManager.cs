using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manages window behavior to simulate 
// how a window manager functions within an operating system
public class WindowManager : MonoBehaviour
{
    //public GameState gameState; // gamestate reference to store data
    public GameObject[] windowPrefabs; // stores references to different types of windows that will spawn according to save data

    public List<GameObject> windowList = new List<GameObject>();
    [SerializeField] private GameObject windowPrefab;
    protected int index { get; private set; }

    private void Start() {
        //gameState = SaveSystem.Load(); // Load saved data
        //if(gameState == null) Debug.LogError("WINDOW MANAGER CLASS : gameState == null!");
        
        // find every ungrouped window prior to game runtime
        // and put it in list
        GameObject[] unGroupedWindows = GameObject.FindGameObjectsWithTag("Window");

        // for(int i = 0; i < unGroupedWindows.Length; i++) {
        //     windowList.Add(unGroupedWindows[i]);
        //     if(gameState.windowPosX[i] != null){
        //         gameState.windowPosX[i] = unGroupedWindows[i].transform.position.x;
        //         gameState.windowPosY[i] = unGroupedWindows[i].transform.position.y;
        //     }
        // }
        UpdateListedSortingOrders(); // make sure all windows are sorted right away! :D
        //gameState.windowCount = unGroupedWindows.Length;
        //RespawnSavedWindows();
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

    public void SpawnWindow(GameObject newWindow) {
        windowList.Add(newWindow);
        Instantiate(newWindow);
    }

    public void RemoveClosedWindowData(GameObject closingWindow) {

    }
    // ILL FIGURE IT OUT LATER (I FORGOT LIST IS DYNAMIC AND I NEED TO FIND ANOTHER WAY)
    // public void SaveDekstopState() { // called whenever scene switches away from desktop
    //     foreach(GameObject wn in windowList) {
    //         if(wn == null) { // remove any null windows
    //             windowList.Remove(wn);
    //             gameState.windowCount--; // reduce window count for next instance
    //         }
    //     }
    //     SaveSystem.Save(gameState);
    // }

    // private void RespawnSavedWindows() { // Respawn and set position of each window based off save-file data
    //     if(gameState.windowPosX.Length == 0 || gameState.windowPosY.Length == 0) return;
    //     for(int i = 0; i < gameState.windowCount; i++) {
    //         GameObject newWin = Instantiate(windowPrefab, new Vector2
    //         (gameState.windowPosX[i], gameState.windowPosY[i]), Quaternion.identity);
    //         windowList[i] = newWin;
    //     }
    // }

    private void UpdateFocusedWindowComponents(GameObject clickedWindow, int parentWindowSortingOrder) { // add
        // Update each window components sorting order
    }
}
