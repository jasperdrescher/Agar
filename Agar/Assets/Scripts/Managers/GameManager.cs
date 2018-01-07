using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Utilities
{
    public enum State { Menu, Preparing, Playing, Paused };

    public State currentState;
    public int currentLevel = 1;
    public float elapsedTime = 0.0f;
    public float playTime = 0.0f;
    public int currentScore = 0;
    public int currentHighScore = 0;

    public string backgroundMusic = "BackgroundMusic";

    private AudioManager audioManager;
    private GameObject level;

    // Awake is always called before any Start functions
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        Load();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time;

        if (currentState == State.Playing)
        {
            playTime += Time.deltaTime;

            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
            }
        }
    }

    /// <summary>
    /// Change the current game state.
    /// </summary>
    public void ChangeState(State state)
    {
        Print("Changing state", "event");

        currentState = state;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PrepareLevel(int level)
    {
        Print("Preparing level", "event");

        currentState = State.Preparing;
        currentLevel = level;
    }

    /// <summary>
    /// Play the game.
    /// </summary>
    public void Play()
    {
        Print("Starting game", "event");

        currentState = State.Playing;
        audioManager.PlaySound(backgroundMusic);
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        Print("Pausing game", "event");

        currentState = State.Paused;
        audioManager.PauseSound(backgroundMusic);
        Time.timeScale = 0;
        Save();
    }

    /// <summary>
    /// Proceed gameplay.
    /// </summary>
    public void Resume()
    {
        currentState = State.Playing;
        audioManager.ResumeSound(backgroundMusic);
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Update variables related to UI.
    /// </summary>
    public void UpdateScore(int a_Score)
    {
        Print("Updating score", "event");

        currentScore += a_Score;
    }

    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {
        Print("Loading", "event");

        currentHighScore = Deserialize<int>(Application.streamingAssetsPath + "/XML/Highscores.xml");
    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        Print("Saving", "event");

        Serialize(currentHighScore, Application.streamingAssetsPath + "/XML/Highscores.xml");
    }

    /// <summary>
    /// Quit the game and save settings.
    /// </summary>
    public void Quit()
    {
        Save();

        Print("Quitting game", "event");

        Application.Quit();
    }
}
