using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public GameObject Food;
    public float SpawnSpeed;

    void Start()
    {
        InvokeRepeating("Generate", 0, SpawnSpeed);
    }

    void Generate()
    {
            int x = Random.Range(0, Camera.main.pixelWidth);
            int y = Random.Range(0, Camera.main.pixelHeight);

            Vector3 Target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
            Target.z = 0;

            Instantiate(Food, Target, Quaternion.identity);
    }
}