using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Utilities
{
    public static Level instance = null;

    public bool persistent = false;
    public Vector2 spawnField;
    public float borderThickness = 1.0f;

    private FoodManager foodManager;
    private GameManager gameManager;
    private BoxCollider2D upCollider;
    private BoxCollider2D downCollider;
    private BoxCollider2D rightCollider;
    private BoxCollider2D leftCollider;

    // Awake is always called before any Start functions
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
            if (persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        foodManager = gameObject.GetComponent<FoodManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (gameManager.currentState == GameManager.State.Preparing)
        {
            PrepareLevel(gameManager.currentLevel);
            gameManager.ChangeState(GameManager.State.Playing);
        }
	}

    /// <summary>
    /// Prepare the current level.
    /// </summary>
    public void PrepareLevel(int a_Level)
    {
        Print("Preparing level", "event");

        ChangeLevel(a_Level);
        foodManager.SpawnFood(50);
    }

    /// <summary>
    /// Change the current level.
    /// </summary>
    public void ChangeLevel(int a_Level)
    {
        Print("Changing level", "event");

        switch (a_Level)
        {
            case 1:
                spawnField = new Vector2(50.0f - borderThickness, 50.0f - borderThickness);
                foodManager.ChangeLimits(200, spawnField);
                break;
            case 2:
                spawnField = new Vector2(50.0f - borderThickness, 50.0f - borderThickness);
                foodManager.ChangeLimits(100, spawnField);
                break;
            default:
                spawnField = new Vector2(50.0f - borderThickness, 50.0f - borderThickness);
                foodManager.ChangeLimits(200, spawnField);
                break;
        }
    }
}
