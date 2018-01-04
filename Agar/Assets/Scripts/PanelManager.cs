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

    private GameObject gameManager;
    private GameManager managerScript;
    private AudioManager audioManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            managerScript.Print("No AudioManager found!", "error");
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
            managerScript.Pause();
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            managerScript.Continue();
            pausePanel.SetActive(false);
        }

        elapsedTimeText.text = managerScript.elapsedTime.ToString("F1");
        scoreText.text = "SCORE: " + managerScript.currentScore;
        highscoreText.text = "HIGH SCORE: " + managerScript.currentHighScore;
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
            managerScript.Print("No audio file found " + a_Sound, "error");
        }
    }
}
