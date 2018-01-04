using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public enum State { Menu, Preparing, Playing, Paused };

    public bool persistent = false;
    public State currentState;
    public int currentLevel = 1;
    public float elapsedTime = 0.0f;
    public float playTime = 0.0f;
    public int currentScore = 0;
    public int currentHighScore = 0;

    private AudioManager audioManager;
    private GameObject level;

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
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

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
    public void ChangeState(State a_newState)
    {
        Print("Changing state", "event");

        currentState = a_newState;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PrepareLevel(int a_Newlevel)
    {
        Print("Preparing level", "event");

        currentState = State.Preparing;
        currentLevel = a_Newlevel;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void LoadScene(string a_Name)
    {
        SceneManager.LoadScene(a_Name);
        PrepareLevel(currentLevel);
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        currentState = State.Paused;
        audioManager.PauseSound("BackgroundMusic");
        Time.timeScale = 0;
        Save();
    }

    /// <summary>
    /// Proceed gameplay.
    /// </summary>
    public void Continue()
    {
        currentState = State.Playing;
        audioManager.ResumeSound("BackgroundMusic");
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

        XmlSerializer serializer = new XmlSerializer(typeof(int));
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/XML/Highscores.xml");
        currentHighScore = (int)serializer.Deserialize(reader.BaseStream);
        reader.Close();
    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        Print("Saving", "event");

        XmlSerializer serializer = new XmlSerializer(typeof(int));
        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/XML/Highscores.xml");
        serializer.Serialize(writer.BaseStream, currentHighScore);
        writer.Close();
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

    /// <summary>
    /// Prints the message in the console with a clear description.
    /// </summary>
    public void Print(string message, string severity)
    {
        switch (severity)
        {
            case "log":
                Debug.Log(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
            case "event":
                Debug.Log("<color=orange>(Event) </color>" + gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
            case "warning":
                Debug.LogWarning(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
            case "error":
                Debug.LogError(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;

            default:
                Debug.Log(gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
                break;
        }
    }
}
