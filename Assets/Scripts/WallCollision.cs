using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public bool onWall; //Checks if player is touching the wall
    public bool onRightWall;  //Checks if player is touching right wall
    public bool onLeftWall;   //Checks if player is touching the left wall
    public float collisionRadius;   
    public Transform rightChecker;  
    public Transform leftChecker;
    public int wallside;
    public LayerMask wallLayer;
    

    // Update is called once per frame
    void Update()
    {
        onWall = Physics2D.OverlapCircle(rightChecker.position, collisionRadius, wallLayer)  //Checks if we are on either wall
            || Physics2D.OverlapCircle(leftChecker.position, collisionRadius, wallLayer); 
        onRightWall = Physics2D.OverlapCircle(rightChecker.position, collisionRadius, wallLayer);    //Checks if we are on right wall
        onLeftWall = Physics2D.OverlapCircle(leftChecker.position, collisionRadius, wallLayer);  //Checks if we are on left wall
        wallside = onRightWall ? 1 : -1;    //If on right wall side = 1 if on left wall, side = -1.
    }

    //You will need left and right when doing animations
}
