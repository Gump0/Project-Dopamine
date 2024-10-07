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
       RaycastHit click;
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


    //    if(Physics2D.Raycast(ray, out click)){
    //     string clickedTag = click.collider.tag;
    //     Debug.Log(clickedTag);

    //     // check what object the player left clicked on
    //     // and execute a specific block of code...
    //         switch(clickedTag){
    //             case "Window":
    //             //yaddayadda
    //             break;
    //         }
    //     }

    }

        public void OnRightMouseClick(){
        Debug.Log("Right click was clicked!!");
    }

    private Vector2 GetMousePosition(){
        Vector2 mousePos = Input.mousePosition;
        return mousePos;
    }
}
