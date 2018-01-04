using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PanelManager : MonoBehaviour
{
    [Header("UI")]
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            gameManager.Print("No AudioManager found!", "error");
        }
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(false);
        fadePanel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
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

    public void TogglePanel(GameObject a_Panel)
    {
        if (a_Panel.activeInHierarchy)
        {
            a_Panel.SetActive(false);
        }
        else
        {
            a_Panel.SetActive(true);
        }
    }

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

    public void PlaySound(string a_Sound)
    {
        if (a_Sound == buttonHover)
        {
            audioManager.PlaySound(buttonHover);
        }
        else if (a_Sound == buttonPress)
        {
            audioManager.PlaySound(buttonPress);
        }
        else
        {
            gameManager.Print("No audio file found " + a_Sound, "error");
        }
    }
}
