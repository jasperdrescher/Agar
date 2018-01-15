using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MenuManager : Utilities
{
    public GameObject pausePanel;
    public bool showFrameRate = true;
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

    void OnGUI()
    {
        if (showFrameRate)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = Time.deltaTime * 1000.0f;
            float fps = 1.0f / Time.deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}
