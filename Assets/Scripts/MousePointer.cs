using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script in charge with handling mouse inputs
// And checkinf if the player clicks a window or  not
public class MousePointer : MonoBehaviour
{
    private WindowManager windowManager;
    private void Awake(){
        if(windowManager == null) windowManager = GetComponent<WindowManager>();
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
            break;

            case "Fullscreen":
            Debug.Log("Fullscreen/Window Button");
            break;

            case "Minimize":
            Debug.Log("Minimize Button");
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
