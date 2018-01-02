using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager managerScript;
    private GameObject foodSpawner;
    private FoodSpawner spawnerScript;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
        foodSpawner = GameObject.Find("FoodSpawner");
        spawnerScript = foodSpawner.GetComponent<FoodSpawner>();

        int foodScore = managerScript.currentScore;
        if (foodScore < 500)
        {
            int increase = 0;
            increase = foodScore / 25;
            transform.localScale += Vector3.one * increase * Time.deltaTime;
        }
        if (foodScore > 499)
        {
            transform.localScale += new Vector3(0.4f, 0.4f, 0f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void RemoveObject()
    {
        spawnerScript.food.Remove(gameObject);
    }
}
