using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWallpaper : MonoBehaviour
{
    public Sprite[] wallpapers;
    public Sprite[] icons;
    public GameState gameState;

    [SerializeField] Image avatarPreview;
    [SerializeField] Image wallpaperPreview;

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
        UpdateAvatarPreview();
    }

    public void DecreaseIconIndex() {
        if(iconIndex != 0) {
            iconIndex--;
        } else {
            iconIndex = icons.Length - 1;
        }
        UpdateAvatarPreview();
    }

    void ChangeWallpaper() { // called by UI element to update sprite
        sr.sprite = wallpapers[wallpaperIndex];
        UpdateWallpaperPreview();
    }

    void UpdateAvatarPreview() {
        avatarPreview.sprite = icons[iconIndex];
    }

    void UpdateWallpaperPreview() {
        wallpaperPreview.sprite = wallpapers[wallpaperIndex];
    }

    void Start() {
        gameState = SaveSystem.Load(); // Load saved data
        sr = GetComponent<SpriteRenderer>();
        
        wallpaperIndex = gameState.playerWallpaperIndex;
        iconIndex = gameState.playerUserImageIndex;
        ChangeWallpaper();
        UpdateAvatarPreview();
    }
}
