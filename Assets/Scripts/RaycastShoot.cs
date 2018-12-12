﻿using UnityEngine;
using System.Collections;

public class RaycastShoot: MonoBehaviour
{
    public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 300f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private AudioSource gunAudio;                                       // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing
    private Animator animator;
    private bool hammerPulled;

    void Start()
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();

        // Get and store a reference to our AudioSource component
        gunAudio = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
    }


    void Update()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            hammerPulled = false;
            animator.SetTrigger("Shot2");

            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            if (laserLine != null)
                laserLine.SetPosition(0, gunEnd.position);

            // Check if our raycast has hit anything
            if (Physics.Raycast(gunEnd.position, gunEnd.transform.forward, out hit, weaponRange))
            {
                // Set the end position for our laser line 
                if (laserLine != null)
                    laserLine.SetPosition(1, hit.point);

                // Get a reference to a health script attached to the collider we hit
                ShootableTarget health = hit.collider.GetComponent<ShootableTarget>();

                // If there was a health script attached
                if (health != null)
                {
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.Damage(gunDamage);
                }

                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                if (laserLine != null)
                    laserLine.SetPosition(1, gunEnd.position + (gunEnd.transform.forward * weaponRange));
            }
        }

        if (Input.GetButtonDown("PullHammerLeft"))
        {
            hammerPulled = true;
            animator.SetTrigger("PrepareForShooting");
        }
    }


    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        if (gunAudio)
        {
            gunAudio.Play();
        }

        // Turn on our line renderer
        if (laserLine != null)
            laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        if (laserLine != null)
            laserLine.enabled = false;
    }
}