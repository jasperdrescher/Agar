using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MenuManager : Utilities
{
    public GameObject pausePanel;
    public Text scoreText;
    public Text highscoreText;
    public Text elapsedTimeText;
    public string buttonHover = "ButtonHover";
    public string buttonPress = "ButtonPress";

    private GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentState != GameManager.State.Menu)
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !pausePanel.activeInHierarchy)
            {
                TogglePause();
            }
            else if (Input.GetKeyUp(KeyCode.Escape) && pausePanel.activeInHierarchy)
            {
                TogglePause();
            }

            elapsedTimeText.text = gameManager.elapsedTime.ToString("F1");
            scoreText.text = "SCORE: " + gameManager.currentScore;
            highscoreText.text = "HIGH SCORE: " + gameManager.currentHighScore;
        }
    }

    /// <summary>
    /// Toggle the pause menu and state.
    /// </summary>
    public void TogglePause()
    {
        if (gameManager.currentState == GameManager.State.Playing)
        {
            TogglePanel(pausePanel);
            gameManager.Pause();
        }
        else
        {
            TogglePanel(pausePanel);
            gameManager.Resume();
        }
    }

    /// <summary>
    /// Toggle a panel.
    /// </summary>
    public void TogglePanel(GameObject panel)
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    /// <summary>
    /// Toggle a canvas.
    /// </summary>
    public void ToggleCanvas(Canvas canvas)
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
}
