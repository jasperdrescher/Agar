using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitMassController : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, Target, movementSpeed * Time.deltaTime / transform.localScale.x);
    }
}