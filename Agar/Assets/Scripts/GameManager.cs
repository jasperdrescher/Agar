using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public float elapsedTime;

    // Use this for initialization
    void Start()
    {
        if (System.IO.File.Exists("Game.dat"))
        {
            PrintToConsole("Game.dat exists", "warning");
        }
        else
        {
            using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Create)))
            {
                writer.Write(GameObject.Find("Player").GetComponent<PlayerController>().currentHighScore);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        UpdateGameLogic();
    }

    /// <summary>
    /// Force the GameManager to update variables
    /// </summary>
    public void UpdateGameLogic()
    {
        elapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            writer.Write(GameObject.Find("Player").GetComponent<PlayerController>().currentHighScore);
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
            GameObject.Find("Player").GetComponent<PlayerController>().currentHighScore = parsedInt;
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
