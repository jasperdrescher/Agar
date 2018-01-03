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
    private BoxCollider2D upCollider;
    private BoxCollider2D downCollider;
    private BoxCollider2D rightCollider;
    private BoxCollider2D leftCollider;

    void Start()
    {
        upCollider = gameObject.AddComponent<BoxCollider2D>();
        upCollider.offset = new Vector2(0.0f, spawnField.y);
        upCollider.size = new Vector2(spawnField.x * 2.0f, 1.0f);
        rightCollider = gameObject.AddComponent<BoxCollider2D>();
        rightCollider.offset = new Vector2(spawnField.x, 0.0f);
        rightCollider.size = new Vector2(1.0f, spawnField.y * 2.0f);
        downCollider = gameObject.AddComponent<BoxCollider2D>();
        downCollider.offset = new Vector2(0.0f, -spawnField.y);
        downCollider.size = new Vector2(spawnField.x * 2.0f, 1.0f);
        leftCollider = gameObject.AddComponent<BoxCollider2D>();
        leftCollider.offset = new Vector2(-spawnField.x, 0.0f);
        leftCollider.size = new Vector2(1.0f, spawnField.y * 2.0f);
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
            newFood.transform.parent = gameObject.transform;
            food.Add(newFood);
        }
    }
}
