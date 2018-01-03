using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public List<GameObject> food = new List<GameObject>();
    public Vector2 spawnField;
    public int maxFood = 100;
    public float spawnInterval = 5.0f;
    private float accumulator;

    void Start()
    {

    }

    void Update()
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

    public void SpawnFood(int a_Amount)
    {
        for (int i = 0; i < a_Amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnField.x, spawnField.x), Random.Range(-spawnField.y, spawnField.y), 0.0f);
            GameObject newFood = Instantiate(foodPrefab, position, Quaternion.identity);
            food.Add(newFood);
        }
    }
}
