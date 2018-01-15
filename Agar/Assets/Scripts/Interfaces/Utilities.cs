using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Utilities : MonoBehaviour
{
    /// <summary>
    /// Serialize an object.
    /// </summary>
    public void Serialize(object item, string filepath)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(filepath);
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
    }

    /// <summary>
    /// Deserialize an object.
    /// </summary>
    public static T Deserialize<T>(string filepath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader(filepath);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }

    /// <summary>
    /// Prints the given message in the console with extra information.
    /// </summary>
    public void Print(string message)
    {
        Debug.Log(gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
    }

    /// <summary>
    /// Prints the given message in the console with extra information.
    /// </summary>
    public void Print(string message, string severity)
    {
        switch (severity)
        {
            case "log":
                Debug.Log(gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
                break;
            case "event":
                Debug.Log("<color=orange>(Event) </color>" + gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
                break;
            case "warning":
                Debug.LogWarning(gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
                break;
            case "error":
                Debug.LogError(gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
                break;

            default:
                Debug.Log(gameObject.name + ": " + message + " @ " + Time.time + " seconds.");
                break;
        }
    }
}
