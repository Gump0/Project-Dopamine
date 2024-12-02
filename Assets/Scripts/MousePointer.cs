using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script in charge with handling mouse inputs
// And checkinf if the player clicks a window or not
public class MousePointer : MonoBehaviour
{
    private WindowManager windowManager;
    private WindowFunctions windowFunctions;
    private void Awake(){ // Grab references
        if(windowManager == null) 
            windowManager = GetComponent<WindowManager>();
        if(windowFunctions == null) 
            windowFunctions = GetComponent<WindowFunctions>();
    }

    public void OnLeftMouseClick(){
       RaycastHit2D[] clicks = Physics2D.RaycastAll(GetMousePosition(), Vector2.down, Mathf.Infinity); // find every obj in raycast
        if(clicks.Length == 0)
            return;

        GameObject topmostWindow = null;
        int highestSortingOrder = int.MinValue;

       foreach(var click in clicks) { // iterate through each one and find the window with the top-most sorting order :D
            SpriteRenderer clickSR = click.collider.GetComponent<SpriteRenderer>();
            if(clickSR != null && clickSR.sortingOrder > highestSortingOrder){
                topmostWindow = click.collider.gameObject;
                highestSortingOrder = clickSR.sortingOrder;
            }
       }

        switch(topmostWindow.tag) {
            case "Window":
            Debug.Log("Clicked Window");
            windowManager.UpdateFocusedWindow(topmostWindow);
            break;

            case "WindowBar":
            Debug.Log("Dragging window bar");
            windowManager.UpdateFocusedWindow(topmostWindow);
            break;

            case "Close":
            Debug.Log("Close Button");
            windowFunctions.CloseWindow(topmostWindow);
            break;

            case "Fullscreen":
            Debug.Log("Fullscreen/Window Button");
            windowFunctions.MinMaxWindow(topmostWindow);
            break;

            case "Minimize":
            Debug.Log("Minimize Button");
            windowFunctions.MinimizeToTaskBar(topmostWindow);
            break;

            case null:
            Debug.Log("Clicked Desktop");
            break;
        }
    }

        public void OnRightMouseClick(){
        Debug.Log("Right click was clicked!!");
    }

    private Vector2 GetMousePosition(){
        Vector2 mousePos = Input.mousePosition;
        Vector2 relativeMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return relativeMousePos;
    }
}
