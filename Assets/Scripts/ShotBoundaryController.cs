using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBoundaryController: MonoBehaviour
{

    // Destroy shots when they exit the play area
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Destroy(collision.gameObject);
    }

    
}
