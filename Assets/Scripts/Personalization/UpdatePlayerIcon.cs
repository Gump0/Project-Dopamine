using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerIcon : MonoBehaviour
{
    public Sprite[] icons;
    public GameState gameState;

    [SerializeField] int iconIndex;

    public void SaveIconIndex() { // called whenever scene is quit to save data
        gameState.playerUserImageIndex = iconIndex;
        SaveSystem.Save(gameState);
    }

    public void IncreaseIndex() {
        if(iconIndex < icons.Length - 1) {
            iconIndex++;
        } else {
            iconIndex = 0;
        }
    }

    public void DecreaseIndex() {
        if(iconIndex != 0) {
            iconIndex--;
        } else {
            iconIndex = icons.Length - 1;
        }
    }

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        iconIndex = gameState.playerUserImageIndex;
    }
}
