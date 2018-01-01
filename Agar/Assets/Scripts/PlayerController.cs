using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : GameManager 
{
    public float PlayerMovementSpeed = 4.0f;
    public float MassSplitMultiplier = 0.5f;

    public GameObject SplitMass;

    public Text CurrentScore;
    public Text CurrentHighScore;
    public float Increase;

    public int Score = 0;
    public int HighScore = 0;

    // Use this for initialization
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, Target, PlayerMovementSpeed * Time.deltaTime / transform.localScale.x);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x * MassSplitMultiplier >= 1.0f)
            {
                transform.localScale = transform.localScale * MassSplitMultiplier;
                GameObject newSplitMass = Instantiate(SplitMass, transform.position + new Vector3(-0.6f, 0.8f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;
            }
            else
            {
                PrintToConsole("Can't split mass!", "log");
            }
        }

        UpdateGameLogic();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            PrintToConsole("Ate food", "log");
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
        else if (other.gameObject.tag == "SplitMass")
        {
            PrintToConsole("Collided with mass", "log");
            transform.localScale = transform.localScale * 2.0f;
            Destroy(other.gameObject);
        }
    }
}
