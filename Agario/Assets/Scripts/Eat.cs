using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[Serializable]
public class Eat : GameMaster
{
    public string Tag;
    public Text CurrentScore;
    public Text CurrentHighScore;
    public float Increase;

    public int Score = 0;
    public int HighScore = 0;

    void Start()
    {
        Load();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag)
        {
            transform.localScale += new Vector3(Increase, Increase, Increase);
            Destroy(other.gameObject);

            Score += 10;
            CurrentScore.text = "SCORE: " + Score;

            if (Score > HighScore)
            {
                HighScore = Score;
            }

            CurrentHighScore.text = "HIGH SCORE: " + HighScore;
        }
    }
}