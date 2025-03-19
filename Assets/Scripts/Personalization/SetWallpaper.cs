using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWallpaper : MonoBehaviour
{
    public GameState gameState;
    public Sprite[] wallpapers;
    int wallpaperIndex;

    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();

        gameState = SaveSystem.Load();
        wallpaperIndex = gameState.playerWallpaperIndex;
        ChangeIcon();
    }

    void ChangeIcon() { // called by UI element to update sprite
        sr.sprite = wallpapers[wallpaperIndex];
    }
}
