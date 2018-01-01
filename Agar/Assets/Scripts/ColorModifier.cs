using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    public List<Color> colors = new List<Color>();

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
    }
}
