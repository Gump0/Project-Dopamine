using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimations : MonoBehaviour
{
    public Sprite[] loadingSprites;
    private SpriteRenderer sr;
    private float elapsedTime, timeInterval = 0.35f;
    private int index = 0;
    
    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update() {
        elapsedTime += Time.deltaTime;
        AnimateLoadingLogo();
    }

    private void AnimateLoadingLogo() {
        if(elapsedTime < timeInterval) return;
        index++;
        int arrayLength = loadingSprites.Length;
        index = index % arrayLength;

        sr.sprite = loadingSprites[index];
        elapsedTime = 0;
    }
}
