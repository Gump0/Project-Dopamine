using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Serializable]
    public struct KeyEvent{
        public string eventActionName;
        public KeyCode defualtKeyCode;
        public UnityEvent eventAction;
    }

    public KeyEvent[] keyEvents;

    void Update(){
        foreach (var keyEvent in keyEvents){
            if (Input.GetKeyDown(keyEvent.defualtKeyCode)){
                keyEvent.eventAction?.Invoke();
            }
        }
    }
}