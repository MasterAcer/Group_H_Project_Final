using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishPointController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When player enters finish point collider, enable upgrade screen and set moveable of character movement script to false
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().setMoveable(false);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().enableUpgradeMenu();
        }   
    }
}
