using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Colour : MonoBehaviour 
{
    // Create a list of materials
    public List<Material> Mats = new List<Material>();

    void Awake()
    {
        // Change the colour randomly
        GetComponent<Renderer>().material = Mats[Random.Range(0, Mats.Count)];
    }
}
