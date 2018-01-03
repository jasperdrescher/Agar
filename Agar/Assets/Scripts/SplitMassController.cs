using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitMassController : MonoBehaviour
{
    public float movementSpeed = 50.0f;

    public Vector2 movement;
    public Vector2 mouseDistance;

    private Rigidbody2D rigidBody2D;

    // Use this for initialization
    void Start ()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        mouseDistance.x = (Input.mousePosition.x - Camera.main.WorldToScreenPoint(gameObject.transform.position).x) * 0.005f;
        mouseDistance.y = (Input.mousePosition.y - Camera.main.WorldToScreenPoint(gameObject.transform.position).y) * 0.005f;
        movement.x = Input.GetAxis("Horizontal") + mouseDistance.x;
        movement.y = Input.GetAxis("Vertical") + mouseDistance.y;
        rigidBody2D.velocity = movement * movementSpeed * Time.deltaTime;
    }
}