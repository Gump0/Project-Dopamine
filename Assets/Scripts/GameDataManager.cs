using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that is in charge with managing data that must be stored across scenes
// Think time elapsed, score obtained in snake, or data related to prevoiusly opened windows.
public class GameDataManager : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
