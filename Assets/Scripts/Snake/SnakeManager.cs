using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    public void PlayerDeath()
    {
        Debug.Log("Player has died!");
        OnPlayerDeath?.Invoke();
    }
}
