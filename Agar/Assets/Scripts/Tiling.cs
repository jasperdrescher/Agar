using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offset = 2;
    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;
    public bool hasUpBuddy = false;
    public bool hasDownBuddy = false;
    public bool reverseScale = false;

    private float spriteWidth = 0.0f;
    private float spriteHeight = 0.0f;
    private Camera cam;
    private Transform tf;

    void Awake()
    {
        cam = Camera.main;
        tf = transform;
    }

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        spriteHeight = sRenderer.sprite.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLeftBuddy || !hasRightBuddy)
        {
            // The cameras extent (half the width) of what the camera can see
            float camHroziontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            // Calculate the X where the camera can see the edge of the sprite
            float edgeVisiblePosRight = (tf.position.x + spriteWidth / 2) - camHroziontalExtent;
            float edgeVisiblePosLeft = (tf.position.x - spriteWidth / 2) + camHroziontalExtent;

            // Check if we can see the edge of element and call make new buddy if we can
            if (cam.transform.position.x >= edgeVisiblePosRight - offset && !hasRightBuddy)
            {
                MakeBuddyHorizontal(1);
                hasRightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePosLeft + offset && !hasLeftBuddy)
            {
                MakeBuddyHorizontal(-1);
                hasLeftBuddy = true;
            }
        }
        if (!hasUpBuddy || !hasDownBuddy)
        {
            // The cameras extent (half the width) of what the camera can see
            float camVerticalExtent = cam.orthographicSize;

            // Calculate the X where the camera can see the edge of the sprite
            float edgeVisiblePosUp = (tf.position.y + spriteHeight / 2) - camVerticalExtent;
            float edgeVisiblePosDown = (tf.position.y - spriteHeight / 2) + camVerticalExtent;

            // Check if we can see the edge of element and call make new buddy if we can
            if (cam.transform.position.y >= edgeVisiblePosUp - offset && !hasDownBuddy)
            {
                MakeBuddyVertical(1);
                hasDownBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePosDown + offset && !hasUpBuddy)
            {
                MakeBuddyVertical(-1);
                hasUpBuddy = true;
            }
        }
    }

    // Right or Left
    void MakeBuddyHorizontal(int RoL)
    {
        // New position for the buddy
        Vector3 pos = new Vector3(tf.position.x + spriteWidth * RoL, tf.position.y, tf.position.z);
        Transform buddy = Instantiate(transform, pos, tf.rotation);

        // If not tilable, reverse the x size of our object for a smooth transition between sprites
        if (reverseScale)
        {
            buddy.localScale = new Vector3(buddy.localScale.x * -1, buddy.localScale.y, buddy.localScale.z);
        }

        buddy.parent = tf.parent;
        if (RoL > 0)
        {
            buddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else
        {
            buddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }

    // Up or Down
    void MakeBuddyVertical(int UoD)
    {
        // New position for the buddy
        Vector3 pos = new Vector3(tf.position.x, tf.position.y + spriteHeight * UoD, tf.position.z);
        Transform buddy = Instantiate(transform, pos, tf.rotation);

        // If not tilable, reverse the x size of our object for a smooth transition between sprites
        if (reverseScale)
        {
            buddy.localScale = new Vector3(buddy.localEulerAngles.x, buddy.localScale.y * -1, buddy.localScale.z);
        }

        buddy.parent = tf.parent;
        if (UoD > 0)
        {
            buddy.GetComponent<Tiling>().hasUpBuddy = true;
        }
        else
        {
            buddy.GetComponent<Tiling>().hasDownBuddy = true;
        }
    }
}
