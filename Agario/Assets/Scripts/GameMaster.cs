using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameMaster : MonoBehaviour {

    //StreamWriter fileWriter = null;
    // Use this for initialization
    void Start()
    {
        if (System.IO.File.Exists("Game.dat"))
        {
            Debug.Log("Game.dat exists");
        }
        else
        {
            using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Create)))
            {
                writer.Write(GameObject.Find("Player").GetComponent<Eat>().HighScore);
            }
        }
    }

    public void Save()
    {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            writer.Write(GameObject.Find("Player").GetComponent<Eat>().HighScore);
        }
    }

    public void Load()
    {
        using (var reader = new BinaryReader(File.Open(Application.persistentDataPath + "/Game.dat", FileMode.Open)))
        {
            var integer = reader.ReadInt32();
            GameObject.Find("Player").GetComponent<Eat>().HighScore = integer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Save();
            Debug.Log("game quitted");
            Application.Quit();
        }
    }
}
