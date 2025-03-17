using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWallpaper : MonoBehaviour
{
    public Sprite[] wallpapers;
    public GameState gameState;

    [SerializeField] int wallpaperIndex;
    SpriteRenderer sr;

    public void SaveWallpaperIndex() { // called whenever scene is quit to save data
        gameState.playerWallpaperIndex = wallpaperIndex;
        SaveSystem.Save(gameState);
    }

    public void IncreaseIndex() {
        if(wallpaperIndex < wallpapers.Length - 1) {
            wallpaperIndex++;
            ChangeWallpaper();
        } else {
            wallpaperIndex = 0;
            ChangeWallpaper();
        }
    }

    public void DecreaseIndex() {
        if(wallpaperIndex != 0) {
            wallpaperIndex--;
            ChangeWallpaper();
        } else {
            wallpaperIndex = wallpapers.Length - 1;
            ChangeWallpaper();
        }
    }

    void ChangeWallpaper() { // called by UI element to update sprite
        sr.sprite = wallpapers[wallpaperIndex];
    }

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        gameState = SaveSystem.Load(); // Load saved data
        
        wallpaperIndex = gameState.playerWallpaperIndex;
        ChangeWallpaper();
    }
}
