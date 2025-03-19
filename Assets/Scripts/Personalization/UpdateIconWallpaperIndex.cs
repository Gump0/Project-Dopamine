using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWallpaper : MonoBehaviour
{
    public Sprite[] wallpapers;
    public Sprite[] icons;
    public GameState gameState;

    [SerializeField] int wallpaperIndex;
    [SerializeField] int iconIndex;
    SpriteRenderer sr;

    public void SaveIndexs() { // called whenever scene is quit to save data
        gameState.playerWallpaperIndex = wallpaperIndex;
        gameState.playerUserImageIndex = iconIndex;
        SaveSystem.Save(gameState);
    }

    public void IncreaseWallpaperIndex() {
        if(wallpaperIndex < wallpapers.Length - 1) {
            wallpaperIndex++;
            ChangeWallpaper();
        } else {
            wallpaperIndex = 0;
            ChangeWallpaper();
        }
    }

    public void DecreaseWallpaperIndex() {
        if(wallpaperIndex != 0) {
            wallpaperIndex--;
            ChangeWallpaper();
        } else {
            wallpaperIndex = wallpapers.Length - 1;
            ChangeWallpaper();
        }
    }

    public void IncreaseIconIndex() {
        if(iconIndex < icons.Length - 1) {
            iconIndex++;
        } else {
            iconIndex = 0;
        }
    }

    public void DecreaseIconIndex() {
        if(wallpaperIndex != 0) {
            iconIndex--;
        } else {
            iconIndex = icons.Length - 1;
        }
    }

    void ChangeWallpaper() { // called by UI element to update sprite
        sr.sprite = wallpapers[wallpaperIndex];
    }

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        sr = GetComponent<SpriteRenderer>();
        
        wallpaperIndex = gameState.playerWallpaperIndex;
        iconIndex = gameState.playerUserImageIndex;
        ChangeWallpaper();
    }
}
