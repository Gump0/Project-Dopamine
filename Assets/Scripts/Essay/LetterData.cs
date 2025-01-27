using System.Collections;
using UnityEngine;

// Class in charge with storing Letter Object Data
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
