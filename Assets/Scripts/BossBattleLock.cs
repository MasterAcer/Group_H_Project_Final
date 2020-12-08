using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleLock : MonoBehaviour
{
    // Destroy any arrow that touches the collider so that
    // the boss cannot be killed before boss battle and
    // the remaining enemies cannot hit player during boss battle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Arrow") || other.CompareTag("Enemy Arrow"))
        {
            Destroy(other.gameObject);
        }
    }

    // turn on collision to stop player form exiting the boss battle 
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().isTrigger = false;
            Camera.main.GetComponent<CameraFollow>().minCameraPos.x = (float) 106.5;
        }
    }
}
