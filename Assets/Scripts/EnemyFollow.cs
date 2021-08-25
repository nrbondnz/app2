using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private Rigidbody rb;
    private bool isPlayerInRange = false;
    //gameobject to hold a reference of the player.
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            // calculate a a vector to determine the dash direction
            // by subtracting the player position from the enemy position
            Vector3 targetDirection = player.transform.position - transform.position;

            //add a force to our enemy Rigidbody, multiplied by speed and fixedDeltaTime for smoothness
            //the force mode is going to be velocity change which means this force will change our velocity
            //ignoring the enemy's mass 
            rb.AddForce(targetDirection * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            //store our velocity in a temp variable to change it later
            Vector3 newVelocity = rb.velocity;

            //remove any velocity on the Y-Axis
            newVelocity.y = 0;

            //set the new velocity to our Rigidbody
            rb.velocity = newVelocity;
        }
    }
}
