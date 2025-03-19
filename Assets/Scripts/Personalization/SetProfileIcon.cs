using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProfileIcon : MonoBehaviour
{
    public GameState gameState;
    public Sprite[] icons;
    int iconIndex;

    Image im;

    void Start() {
        im = GetComponent<Image>();

        gameState = SaveSystem.Load();
        iconIndex = gameState.playerUserImageIndex;
        ChangeIcon();
    }

    void ChangeIcon() { // called by UI element to update sprite
        if(iconIndex < 0)   //failsafe
            iconIndex = 0;
        im.sprite = icons[iconIndex];
    }
}
