using System;
using UnityEngine;

// This class handles storing data that interacts with the
// SaveSystem class, where other scripts access this for
// necessary information.
public class GameState
{
    public float[][] windowPositions { get; set; } // Stores the position of every window in the desktop scene
    public int snakeHighScore { get; set; } // tracks snake game highscore
    public int currentEssayProgress { get; set; } // stores essay char index value
    public int clickerScore { get; set; } // stores players clicker game score
    public string playerWallpaper { get; set; } // stores players wallpaper sprite image in Base64 format
    public string playerUserImage { get; set; } // stores players profile picture sprite image in Base64 format

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
