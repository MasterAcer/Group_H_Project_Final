using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    // Transform component of player
    public Transform Player;

    public bool bounds;

    // Minimum and maximum positions of camera
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
        if(Player != null)
        {
            //changes camera position to only follow the player's x co-ordinate position.
            transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);

            //if bounds == true, then the camera cannot leave these bounds set in unity
            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                    Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                    Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }
        }
        
    }

    
}
