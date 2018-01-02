using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitMassController : MonoBehaviour
{
    public float movementSpeed = 50.0f;

    private Vector2 movement;

    private Rigidbody2D rigidBody2D;

    // Use this for initialization
    void Start ()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X");
        movement.y = Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y");
        rigidBody2D.AddForce(movement * movementSpeed * Time.deltaTime);
    }
}