using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{
    public float PlayerSpeed;

    void Update()
    {
        Vector3 Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Target.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, Target, PlayerSpeed * Time.deltaTime / transform.localScale.x);
    }
}
