using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
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
                Debug.Log("<color=orange>(Event) </color>" + gameObject.name + ": " + message + " @ " + Time.time + " seconds");
                break;
            case "warning":
                Debug.LogWarning(gameObject.name + ": " + message + " @ " + Time.time + " seconds");
                break;
            case "error":
                Debug.LogError(gameObject.name + ": " + message + " @ " + Time.time + " seconds");
                break;

            default:
                Debug.Log(gameObject.name + ": " + message + " @ " + Time.time + " seconds");
                break;
        }
    }
}
