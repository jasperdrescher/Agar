using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class MenuManager : Utilities
{
    public GameObject fadePanel;
    public GameObject gameplayPanel;
    public GameObject pausePanel;
    public Text scoreText;
    public Text highscoreText;
    public Text elapsedTimeText;
    public string buttonHover = "ButtonHover";
    public string buttonPress = "ButtonPress";

    private GameManager gameManager;
    private AudioManager audioManager;

    // Use this for initialization
    void Start ()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }

        gameManager = GameManager.instance;
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
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
    /// Load a scene based on the given name.
    /// </summary>
    public void LoadScene(string name)
    {
        gameManager.LoadScene(name);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void Quit()
    {
        gameManager.Quit();
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
            gameManager.Continue();
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

    /// <summary>
    /// Request the AudioManager to play a sound.
    /// </summary>
    public void PlaySound(string sound)
    {
        if (sound == buttonHover)
        {
            audioManager.PlaySound(buttonHover);
        }
        else if (sound == buttonPress)
        {
            audioManager.PlaySound(buttonPress);
        }
        else
        {
            Print("No audio file found " + sound, "error");
        }
    }
}
