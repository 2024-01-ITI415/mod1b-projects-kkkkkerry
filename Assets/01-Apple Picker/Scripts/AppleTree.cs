using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // prefab for instantiating apples
    public GameObject appleprefab;

    // speed at which appletree moves
    public float speed = 1f;

    // Distance where appletree turns around
    public float leftAndRightEdge = 10f;

    // chance that the appletree will change the direction
    public float chanceToChangeDirections = 0.1f;

    // Rate at which apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Drooping apple every seconds
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(appleprefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    // Update is called once per frame
    void Update()
    {
       // Basic Movement
       Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); //Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
             speed = -Mathf.Abs(speed); // Move left
        }
    }
    void FixedUpdate()
    {
        // Changing direction randomly is now time-based because of FixedUpdate()
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change Direction 
        }
    }
}
