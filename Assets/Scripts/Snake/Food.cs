using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SNAKE GAME CODE!
// Courtesy to Zach for getting this working
public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public int foodCount = 0;
    public Text currentScoreText, highScoreText;

    SnakeManager sm;

    private void Start() {
        SnakeManager sm = GameObject.Find("SnakeManager").GetComponent<SnakeManager>();
        gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
        RandomizePosition();
        SnakeManager.OnPlayerDeath += ResetFoodCount;
        highScoreText.text = "High Score: " + sm.highScore.ToString();
    }

    private void RandomizePosition() {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { 
        RandomizePosition();
            foodCount++;
            currentScoreText.text = "Score: " + foodCount.ToString();
        }
        UpdateHighScore();
    }

    public void ResetFoodCount() {
        foodCount = 0;
        currentScoreText.text = "Score: " + foodCount.ToString();
    }

    private void OnDestroy() {
        SnakeManager.OnPlayerDeath -= ResetFoodCount;
    }

    public void UpdateHighScore() {
        SnakeManager sm = GameObject.Find("SnakeManager").GetComponent<SnakeManager>();
        if(sm == null){
            Debug.LogError("FOOD CLASS : SnakeManager Reference is NULL!");
            return;
        }
        highScoreText.text = "High Score: " + sm.highScore.ToString();
        if(foodCount >= sm.highScore)
        sm.highScore++;
    }
}
