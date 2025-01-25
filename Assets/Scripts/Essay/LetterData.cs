using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterData : MonoBehaviour
{
    public Sprite sprite { get; set; }       // assinged alphabet sprite
    public char character { get; set; }      // assigned char
    public bool isTyped { get; set; }        // checks if the letter has been typed

    public void UpdateSprite(Sprite sprite) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }
}
