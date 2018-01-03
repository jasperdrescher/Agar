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

    private GameObject gameManager;
    private GameManager managerScript;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(false);
        fadePanel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            managerScript.PauseGame();
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            managerScript.ContinueGame();
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
}
