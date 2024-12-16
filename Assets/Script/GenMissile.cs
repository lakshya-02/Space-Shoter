using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenMissile : MonoBehaviour
{
    [Header("Missile Settings")]
    public float missileSpeed = 5f; // Speed of the missile (adjustable in editor)
    public GameObject impactEffect; // Particle effect prefab for collision

    private void Start()
    {
        // Ignore collision between this missile and other missiles
        IgnoreSelfCollision();

        // Ensure the missile is not rotating and remains straight
        transform.rotation = Quaternion.identity; // No rotation
    }

    private void Update()
    {
        // Move the missile downward at the specified speed
        transform.Translate(Vector3.down * missileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("missile"))
        {
            HandleCollisionWithPlayerMissile(collision);
        }
        else if (collision.CompareTag("Player"))
        {
            HandleCollisionWithPlayer();
        }
    }

    private void HandleCollisionWithPlayerMissile(Collider2D collision)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(collision.gameObject); // Destroy player's missile
        Destroy(this.gameObject); // Destroy this missile
    }

    private void HandleCollisionWithPlayer()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        // Handle player damage logic here (you can add your own logic)

        Destroy(this.gameObject); // Destroy this missile after hitting player
    }

    private void IgnoreSelfCollision()
    {
        Collider2D missileCollider = GetComponent<Collider2D>();
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("missile");

        foreach (GameObject missile in missiles)
        {
            if (missile != this.gameObject) // Ignore self
            {
                Collider2D otherMissileCollider = missile.GetComponent<Collider2D>();
                if (otherMissileCollider != null)
                {
                    Physics2D.IgnoreCollision(missileCollider, otherMissileCollider);
                }
            }
        }
    }
}