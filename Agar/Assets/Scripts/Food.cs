using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        int foodScore = GameObject.Find("Player").GetComponent<PlayerController>().currentScore;
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
}
