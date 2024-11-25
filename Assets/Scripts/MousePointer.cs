using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script in charge with handling mouse inputs
// And checkinf if the player clicks a window or  not
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
       RaycastHit2D click = Physics2D.Raycast(GetMousePosition(), Vector2.down, Mathf.Infinity);
        switch(click.collider.tag){
            case "Window":
            Debug.Log("Clicked Window");
            windowManager.UpdateFocusedWindow(click.collider.gameObject);
            break;

            case "WindowBar":
            Debug.Log("Dragging window bar");
            windowManager.UpdateFocusedWindow(click.collider.gameObject);
            break;

            case "Close":
            Debug.Log("Close Button");
            windowFunctions.CloseWindow(click.collider.gameObject);
            break;

            case "Fullscreen":
            Debug.Log("Fullscreen/Window Button");
            break;

            case "Minimize":
            Debug.Log("Minimize Button");
            windowFunctions.MinimizeToTaskBar(click.collider.gameObject);
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
