using System;
using UnityEngine;

// This class handles storing data that interacts with the
// SaveSystem class, where other scripts access this for
// necessary information.
public class GameState
{
    public float?[] windowPosX { get; set; }         // Stores the position of every window in the desktop scene
    = new float?[] { 0, 3, -3.75f, null, null };     // Defualt values
    public float?[] windowPosY { get; set; }
    = new float?[] { 0, -1, 2.25f, null, null };     // Defualt values
    public int?[] windowType { get; set; }           // Tracks what window type each saved window was according to index stored in window manager

    public int snakeHighScore { get; set; }         // tracks snake game highscore

    public int essayCharIndex { get; set; }         // stores essay char index value
    public bool essayComplete { get; set; }          // stores boolean that checks if essay has been complete

    public int totalClickerScore { get; set; }      // stores players clicker game score
    public int clickerUpgrade { get; set; }         // stores players clicker game upgrade value
    public bool hasMedal { get; set; }              // tracks of player has medal

    public string playerWallpaper { get; set; }     // stores players wallpaper sprite image in Base64 format
    public string playerUserImage { get; set; }     // stores players profile picture sprite image in Base64 format

    // Converts Sprite to Base64
    public void SetSprite(string property, Sprite sprite) {
        if (sprite == null) return;

        Texture2D texture = sprite.texture;
        byte[] bytes = texture.EncodeToPNG();
        string base64 = Convert.ToBase64String(bytes);

        if (property == "wallpaper") playerWallpaper = base64;
        else if (property == "userImage") playerUserImage = base64;
    }

    // Converts Base64 back to Sprite
    public Sprite GetSprite(string property) {
        string base64 = property == "wallpaper" ? playerWallpaper : playerUserImage;
        if (string.IsNullOrEmpty(base64)) return null;

        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
