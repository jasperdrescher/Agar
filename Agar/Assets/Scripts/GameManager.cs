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

    [Header("Progress")]
    public float elapsedTime;
    public int currentScore = 0;
    public int currentHighScore = 0;

    private bool isPlaying = false;

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
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            elapsedTime += Time.deltaTime;

            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
            }
        }
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PrepareLevel()
    {
        PrintToConsole("Preparing level", "event");
        GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>().SpawnFood(50);
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void Play()
    {
        Time.timeScale = 1;
        isPlaying = true;
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPlaying = false;
        Save();
    }

    /// <summary>
    /// Proceed gameplay.
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        isPlaying = true;
    }

    /// <summary>
    /// Update variables related to UI.
    /// </summary>
    public void UpdateScore(int a_Score)
    {
        PrintToConsole("Updating score", "event");

        currentScore += a_Score;
    }

    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {
        PrintToConsole("Loading", "event");

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
        PrintToConsole("Saving", "event");

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
            case "event":
                Debug.Log("<color=orange>(Event) </color>" + gameObject.name + ": " + message + " @ " + elapsedTime + " seconds");
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
