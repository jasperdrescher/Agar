using UnityEngine;
using System.Collections;

public class PlayerController : Utilities
{
    public GameObject splitMass;

    public float movementSpeed = 50.0f;
    public float maxMovementSpeed = 3.0f;
    public float massSplitMultiplier = 0.5f;
    public float increase = 0.05f;
    public Vector2 movement;
    public Vector2 mouseDistance;
    public string eatSound = "EatSound";
    public string spawnSound = "SpawnSound";
    public string mergeSound = "MergeSound";

    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        mouseDistance.x = (Input.mousePosition.x - Camera.main.WorldToScreenPoint(gameObject.transform.position).x) * 0.005f;
        mouseDistance.y = (Input.mousePosition.y - Camera.main.WorldToScreenPoint(gameObject.transform.position).y) * 0.005f;
        movement.x = Input.GetAxis("Horizontal") + mouseDistance.x;
        movement.y = Input.GetAxis("Vertical") + mouseDistance.y;
        movement.x = Mathf.Clamp(movement.x, -maxMovementSpeed, maxMovementSpeed);
        movement.y = Mathf.Clamp(movement.y, -maxMovementSpeed, maxMovementSpeed);
        rigidBody2D.velocity = movement * movementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x * massSplitMultiplier >= 1.0f)
            {
                audioManager.PlaySound(mergeSound);
                transform.localScale = transform.localScale * massSplitMultiplier;
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-0.6f, 0.8f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;
            }
            else
            {
                Print("Can't split mass!", "log");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            audioManager.PlaySound(eatSound);
            transform.localScale += new Vector3(increase, increase, 0);
            other.GetComponent<Food>().RemoveObject();
            gameManager.ChangeScore(10);
        }
        else if (other.gameObject.tag == "SplitMass")
        {
            Print("Collided with mass", "log");
            audioManager.PlaySound(mergeSound);
            transform.localScale = transform.localScale * 2.0f;
            Destroy(other.gameObject);
        }
    }
}
