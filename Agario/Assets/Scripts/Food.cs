using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        int FoodScore = GameObject.Find("Player").GetComponent<Eat>().Score;
        if (FoodScore < 500)
        {
            int Increase = 0;
            Increase = FoodScore / 25;
            transform.localScale += Vector3.one * Increase * Time.deltaTime;
        }
        if (FoodScore > 499)
        {
            transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
