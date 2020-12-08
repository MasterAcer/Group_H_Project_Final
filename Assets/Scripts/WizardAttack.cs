using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    public GameObject shot; // shot object
    public Transform shotSpawnPoint; // spawn point for shots

    private float nextFire; // delay between next shot

    private Vector2 lookDirection;
    private float lookAngle;

    private CharacterController wizard; //CharacterController script for Wizard;

    public float timeBtwShield = 0;
    public float startTimeBtwShield;

    public GameObject shield;
    private bool activeBlock;



    // Start is called before the first frame update
    void Start()
    {
        wizard = GetComponent<CharacterController>();
        
        activeBlock = false;
        shield.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate shot spawn point relative to mouse position
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        shotSpawnPoint.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        // Fire when left mouse button clicked
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire && !(activeBlock))
        {
            shoot();
        }

        Shield();

    }

    // Set time allowed for next shot and start coroutine
    void shoot()
    {

        nextFire = Time.time + wizard.fireRate;
        StartCoroutine(Shoot());

    }

    // Instantiate shots based on number of shots allowed
    IEnumerator Shoot()
    {
        for (int i = 1; i <= wizard.numberOfShots; i++)
        {
            Instantiate(shot, shotSpawnPoint.position, shotSpawnPoint.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Shield()    //player takes no damage when shield is up, delay after the shield is up.
    {
        if (Input.GetMouseButton(1))    //if right mouse button is clicked, show the shield, remove the sword, and dont take any damage.                    
        {                               //When let go of the shield, sword shows again, and timer starts till you can put shield up again.
            if (timeBtwShield <= 0)
            {
                timeBtwShield = startTimeBtwShield;
                shield.GetComponent<Renderer>().enabled = true;
                activeBlock = true;
                wizard.speed = 2.5f;
                wizard.jumpForce = 1;
            }
        }
        else
        {
            activeBlock = false;
            shield.GetComponent<Renderer>().enabled = false;
            timeBtwShield -= Time.deltaTime;
            wizard.speed = 5f;
            wizard.jumpForce = wizard.setJumpForce;
        }
    }

    public bool getActiveBlock()
    {
        return activeBlock;
    }

}
