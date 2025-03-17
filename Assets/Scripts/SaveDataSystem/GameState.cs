using System;
using UnityEngine;

// This class handles storing data that interacts with the
// SaveSystem class, where other scripts access this for
// necessary information.
public class GameState
{
    public float maxGameTime = 600;                  // Stores the maximum playtime in seconds

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

    public int playerWallpaperIndex { get; set; }   // stores players wallpaper index
    public int playerUserImageIndex { get; set; }   // stores players profile picture index
}
