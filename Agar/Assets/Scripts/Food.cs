using UnityEngine;
using System.Collections;

public class Food : Utilities
{
    private GameManager gameManager;
    private FoodManager foodManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        foodManager = FindObjectOfType<FoodManager>();

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (foodManager == null)
        {
            Print("No FoodManager found!", "error");
        }

        int foodScore = gameManager.currentScore;
        if (foodScore < 500)
        {
            int increase = 0;
            increase = foodScore / 25;
            transform.localScale += Vector3.one * increase * Time.deltaTime;
        }
        if (foodScore > 499)
        {
            transform.localScale += new Vector3(0.4f, 0.4f, 0f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void RemoveObject()
    {
        foodManager.food.Remove(gameObject);
        Destroy(gameObject);
    }
}
