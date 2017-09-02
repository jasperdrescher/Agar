﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public float ElapsedTime;

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
                writer.Write(GameObject.Find("Player").GetComponent<PlayerController>().HighScore);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
            PrintToConsole("Game has been quit.", "warning");
            Application.Quit();
        }

        UpdateGameLogic();
    }

    /// <summary>
    /// Force the GameManager to update variables
    /// </summary>
    public void UpdateGameLogic()
    {
        ElapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            writer.Write(GameObject.Find("Player").GetComponent<PlayerController>().HighScore);
        }
    }


    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {
        using (var reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            var integer = reader.ReadInt32();
            GameObject.Find("Player").GetComponent<PlayerController>().HighScore = integer;
        }
    }

    /// <summary>
    /// Prints the message in the console with a clear description.
    /// </summary>
    public void PrintToConsole(string message, string severity)
    {
        if (severity == "log")
        {
            Debug.Log(gameObject.name + ": " + message + " @ " + ElapsedTime + " seconds");
        }
        else if (severity == "warning")
        {
            Debug.Log("<color=yellow>(Warning) </color>" + gameObject.name + ": " + message + " @ " + ElapsedTime + " seconds");
        }
        else if (severity == "error")
        {
            Debug.Log("<color=red>(Error) </color>" + gameObject.name + ": " + message + " @ " + ElapsedTime + " seconds");
        }
        else
        {
            Debug.Log(gameObject.name + ": " + message + " @ " + ElapsedTime + " seconds");
        }
    }
}
