using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI; // The static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; // The desired Z pos of the camera

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        // if there is only one line following an if, it doesn't need braces
        // if (POI == null) return; //return if there is no poi

        // Get position of the poi
        //  Vector3 destination = POI.transform.position;
        Vector3 destination;
        // If there is no POI, return to P:[0, 0, 0]
        if (POI == null)
        {
            destination = Vector3.zero;
        } else
        {
            // Get the position of the poi
            destination = POI.transform.position;
            // If poi is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                // If it is sleeping (not moving)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    POI = null;
                    // in the next update
                    return;
                }
            }
        }
        // Limit the X & Y to minium values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Interpolate deom the current Camera position toward destination;
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Force destination.z to be camZ to keep the camera for enough away
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        // Set the orthographicSize of the camera to keep ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
