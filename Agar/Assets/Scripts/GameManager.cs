using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Progress")]
    public float elapsedTime;
    public int currentScore = 0;
    public int currentHighScore = 0;

    [Header("UI")]
    public GameObject gameplayPanel;
    public GameObject pausePanel;
    public Text scoreText;
    public Text highscoreText;
    public Text elapsedTimeText;

    // Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(false);

        if (System.IO.File.Exists("Game.dat"))
        {
            PrintToConsole("Game.dat exists, loading data", "warning");
            Load();
            UpdateUI(0);
        }
        else
        {
            using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Create)))
            {
                writer.Write(currentHighScore);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            PauseGame();
            Save();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            ContinueGame();
        }

        elapsedTime += Time.deltaTime;
        elapsedTimeText.text = elapsedTime.ToString("F1");
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Proceed gameplay.
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Update variables related to UI.
    /// </summary>
    public void UpdateUI(int a_Score)
    {
        currentScore += a_Score;

        if (currentScore > currentHighScore)
        {
            currentHighScore = currentScore;
        }

        scoreText.text = "SCORE: " + currentScore;
        highscoreText.text = "HIGH SCORE: " + currentHighScore;
    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            writer.Write(currentHighScore);
        }
    }


    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {
        using (var reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            int parsedInt = reader.ReadInt32();
            currentHighScore = parsedInt;
        }
    }

    /// <summary>
    /// Quit the game and save settings.
    /// </summary>
    public void Quit()
    {
        PrintToConsole("Saving game", "warning");
        Save();
        PrintToConsole("Exiting game", "warning");
        Application.Quit();
    }

    /// <summary>
    /// Prints the message in the console with a clear description.
    /// </summary>
    public void PrintToConsole(string message, string severity)
    {
        switch (severity)
        {
            case "log":
                Debug.Log(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
            case "warning":
                Debug.Log("<color=yellow>(Warning) </color>" + gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
            case "error":
                Debug.Log("<color=red>(Error) </color>" + gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;

            default:
                Debug.Log(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
        }
    }
}
