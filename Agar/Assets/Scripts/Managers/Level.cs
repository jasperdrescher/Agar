using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Utilities
{
    public Vector2 spawnField;
    public float borderThickness = 1.0f;
    public GameObject foodPrefab;
    public GameObject tilePrefab;
    public List<GameObject> tiles = new List<GameObject>();
    public List<GameObject> food = new List<GameObject>();
    public float spawnInterval = 5.0f;
    public int initialFoodAmount = 100;
    
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D upCollider;
    private BoxCollider2D downCollider;
    private BoxCollider2D rightCollider;
    private BoxCollider2D leftCollider;
    private float accumulator;
    private int maxFood = 100;
    private float spriteWidth;
    private float spriteHeight;
    private int tileCount;

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
        spriteRenderer = tilePrefab.GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
        spriteHeight = spriteRenderer.sprite.bounds.size.y;
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
        print("Spawning food: " + amount);
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

    public void PrepareLevel()
    {
        if (spriteWidth > 0.0f && spriteHeight > 0.0f)
        {
            float tilesOnX = (spawnField.x * 2) / spriteWidth;
            float tilesOnY = (spawnField.y * 2) / spriteHeight;
            tileCount = Mathf.CeilToInt(tilesOnX) * Mathf.CeilToInt(tilesOnY);

            Print("Spawning " + tileCount + " tiles");

            for (int y = 0; y < tilesOnY + 1; y++)
            {
                for (int x = 0; x < tilesOnX + 1; x++)
                {
                    Vector3 position = new Vector3(-spawnField.x + ((spriteWidth) * x ), -spawnField.y + ((spriteHeight) * y), 0.0f);
                    GameObject newTile = Instantiate(tilePrefab, position, Quaternion.identity);
                    newTile.transform.parent = gameObject.transform;
                    tiles.Add(newTile);
                }
            }
        }
        else
        {
            Print("Tile prefab contains no data!", "error");
        }

        SpawnFood(initialFoodAmount);
    }
}
