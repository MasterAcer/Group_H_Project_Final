using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerWeaponController : MonoBehaviour
{


    public GameObject shot; // shot object
    public Transform shotSpawnPoint; // spawn point for shots

    private float nextFire; // delay between next shot

    private Vector2 lookDirection;
    private float lookAngle;

    private CharacterController ranger; //CharacterController script for ranger;
    private bool activeBlock;

    private float timeCounter = 0.5f;

    SpriteRenderer SpriteRen;
    Color temp;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRen = GetComponent<SpriteRenderer>();
        temp = SpriteRen.GetComponent<SpriteRenderer>().color;
        ranger = GetComponent<CharacterController>();
        activeBlock = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate shot spawn point relative to mouse position
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        shotSpawnPoint.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        // Fire when left mouse button clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            shoot();
        }

        Counter();

    }

    // Set time allowed for next shot and start coroutine
    void shoot()
    {
 
        nextFire = Time.time + ranger.fireRate;
        StartCoroutine(Shoot());

    }

    // Instantiate shots based on number of shots allowed
    IEnumerator Shoot()
    {
        for(int i = 1; i <= ranger.numberOfShots; i++)
        {
            Instantiate(shot, shotSpawnPoint.position, shotSpawnPoint.rotation);
            yield return new WaitForSeconds(0.10f);
        }
    }

    public void Counter()   //If right mouse button is clicked, active counter is on
    {
        if (Input.GetMouseButtonDown(1))
        {
            activeBlock = true;
            temp.a = 0.8f;
            SpriteRen.GetComponent<SpriteRenderer>().color = temp;
            Invoke("Countered", timeCounter);   //Makes counter work more often and feels better
        }

    }

    void Countered()
    {
        activeBlock = false;
        temp.a = 1f;
        SpriteRen.GetComponent<SpriteRenderer>().color = temp;
    }

    public bool getActiveBlock()
    {
        return activeBlock;
    }

}
