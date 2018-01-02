using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public GameObject splitMass;

    public float movementSpeed = 50.0f;
    public float massSplitMultiplier = 0.5f;
    public float increase = 0.05f;

    private Vector2 movement;

    private Rigidbody2D rigidBody2D;
    private GameObject gameManager;
    private GameManager managerScript;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        managerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X");
        movement.y = Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y");
        rigidBody2D.AddForce(movement * movementSpeed * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x * massSplitMultiplier >= 1.0f)
            {
                transform.localScale = transform.localScale * massSplitMultiplier;
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-0.6f, 0.8f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;
            }
            else
            {
                managerScript.PrintToConsole("Can't split mass!", "log");
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            managerScript.PrintToConsole("Ate food", "log");
            transform.localScale += new Vector3(increase, increase, 0);
            other.GetComponent<Food>().RemoveObject();
            managerScript.UpdateUI(10);
        }
        else if (other.gameObject.tag == "SplitMass")
        {
            managerScript.PrintToConsole("Collided with mass", "log");
            transform.localScale = transform.localScale * 2.0f;
            Destroy(other.gameObject);
        }
    }
}
