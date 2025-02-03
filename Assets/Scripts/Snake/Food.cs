using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SNAKE GAME CODE!
// Courtesy to Zach for getting this working
public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public int FoodCount = 0;
    public Text FoodCountText;

    private void Start()
    {
        gridArea = GameObject.Find("GridArea").GetComponent<BoxCollider2D>();
        RandomizePosition();
        SnakeManager.OnPlayerDeath += ResetFoodCount;
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") { 
        RandomizePosition();
            FoodCount++;
            FoodCountText.text = FoodCount.ToString("0");
        }
        
    }

    public void ResetFoodCount()
    {
        FoodCount = 0;
        FoodCountText.text = FoodCount.ToString("0");
    }

    private void OnDestroy()
    {
        SnakeManager.OnPlayerDeath -= ResetFoodCount;
    }
}
