using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutdownButton : MonoBehaviour
{
    public void QuitTheGame()
    {
        Application.Quit();
    }
}