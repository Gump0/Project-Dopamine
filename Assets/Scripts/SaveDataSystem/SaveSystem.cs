using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// This class handles saving data and writing
// to to a corresponding data JSON save-file    
public static class SaveSystem
{
    private static readonly string savePath = Path.Combine(Application.persistentDataPath, "savefile.json");

    public static void Save(GameState gameState)
    {
        try {
            string json = JsonConvert.SerializeObject(gameState, Formatting.Indented);
            File.WriteAllText(savePath, json);
            Debug.Log($"Game saved to {savePath}");
        }
        catch (Exception e) {
            Debug.LogError($"Save failed: {e.Message}");
        }
    }

    public static GameState Load()
    {
        if (!File.Exists(savePath)) {
            Debug.LogWarning("No save file found, returning default GameState.");
            return new GameState();
        }

        try {
            string json = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<GameState>(json);
        }
        catch (Exception e) {
            Debug.LogError($"Load failed: {e.Message}");
            return new GameState();
        }
    }
}