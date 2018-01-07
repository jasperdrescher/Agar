using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Utilities
{
    public Vector2 spawnField;
    public float borderThickness = 1.0f;
    public GameObject foodPrefab;
    public List<GameObject> food = new List<GameObject>();
    public float spawnInterval = 5.0f;
    
    private GameManager gameManager;
    private BoxCollider2D upCollider;
    private BoxCollider2D downCollider;
    private BoxCollider2D rightCollider;
    private BoxCollider2D leftCollider;
    private float accumulator;
    private int maxFood = 100;

    // Awake is always called before any Start functions
    void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        upCollider = gameObject.AddComponent<BoxCollider2D>();
        upCollider.offset = new Vector2(0.0f, spawnField.y);
        upCollider.size = new Vector2(spawnField.x * 2.0f, borderThickness);
        rightCollider = gameObject.AddComponent<BoxCollider2D>();
        rightCollider.offset = new Vector2(spawnField.x, 0.0f);
        rightCollider.size = new Vector2(borderThickness, spawnField.y * 2.0f);
        downCollider = gameObject.AddComponent<BoxCollider2D>();
        downCollider.offset = new Vector2(0.0f, -spawnField.y);
        downCollider.size = new Vector2(spawnField.x * 2.0f, borderThickness);
        leftCollider = gameObject.AddComponent<BoxCollider2D>();
        leftCollider.offset = new Vector2(-spawnField.x, 0.0f);
        leftCollider.size = new Vector2(borderThickness, spawnField.y * 2.0f);
    }
	
	// Update is called once per frame
	void Update ()
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
