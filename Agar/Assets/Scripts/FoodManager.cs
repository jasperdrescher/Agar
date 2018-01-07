using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : Utilities
{
    public GameObject foodPrefab;
    public List<GameObject> food = new List<GameObject>();
    public float spawnInterval = 5.0f;

    private GameManager gameManager;
    private float accumulator;
    private int maxFood = 100;
    private Vector2 spawnField;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentState == GameManager.State.Playing)
        {
            if (accumulator > spawnInterval)
            {
                if (food.Count < maxFood)
                {
                    SpawnFood(1);
                }

                accumulator = 0;
            }

            accumulator += Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawn a certain amount of food instances.
    /// </summary>
    public void SpawnFood(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnField.x, spawnField.x), Random.Range(-spawnField.y, spawnField.y), 0.0f);
            GameObject newFood = Instantiate(foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = gameObject.transform;
            food.Add(newFood);
        }
    }

    /// <summary>
    /// Change the current variables in FoodManager.
    /// </summary>
    public void ChangeLimits(int a_Maxfood, Vector2 a_Maxfield)
    {
        maxFood = a_Maxfood;
        spawnField = a_Maxfield;
    }
}
