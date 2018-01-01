using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : GameManager 
{
    public GameObject splitMass;
    public Text scoreText;
    public Text highscoreText;
    public Text elapsedTimeText;

    public float movementSpeed = 4.0f;
    public float massSplitMultiplier = 0.5f;
    public float increase = 0.05f;
    public int currentScore = 0;
    public int currentHighScore = 0;

    // Use this for initialization
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime / transform.localScale.x);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x * massSplitMultiplier >= 1.0f)
            {
                transform.localScale = transform.localScale * massSplitMultiplier;
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-0.6f, 0.8f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;
            }
            else
            {
                PrintToConsole("Can't split mass!", "log");
            }
        }

        elapsedTimeText.text = elapsedTime.ToString("F1");
        UpdateGameLogic();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            PrintToConsole("Ate food", "log");
            transform.localScale += new Vector3(increase, increase, 0);
            Destroy(other.gameObject);

            currentScore += 10;
            scoreText.text = "SCORE: " + currentScore;

            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
            }

            highscoreText.text = "HIGH SCORE: " + currentHighScore;
        }
        else if (other.gameObject.tag == "SplitMass")
        {
            PrintToConsole("Collided with mass", "log");
            transform.localScale = transform.localScale * 2.0f;
            Destroy(other.gameObject);
        }
    }
}
