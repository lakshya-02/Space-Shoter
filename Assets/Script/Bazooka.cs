using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    public float baseMissileSpeed = 5f; // Base speed of the bazooka missile
    private float currentMissileSpeed;
    public GameObject impactEffect; // Particle effect prefab for collision

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        UpdateSpeed();

        // Ignore collision between the bazooka and objects tagged as "Wall"
        Collider2D bazookaCollider = GetComponent<Collider2D>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            if (wallCollider != null)
            {
                Physics2D.IgnoreCollision(bazookaCollider, wallCollider);
            }
        }
    }

    private void Update()
    {
        UpdateSpeed();

        // Move the bazooka missile upwards relative to its rotated direction
        transform.Translate(Vector3.up * currentMissileSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        if (gameManager != null)
        {
            int score = gameManager.GetCurrentScore();

            if (score <= 100)
            {
                currentMissileSpeed = baseMissileSpeed;
            }
            else if (score <= 200)
            {
                currentMissileSpeed = baseMissileSpeed + 2f;
            }
            else if (score <= 300)
            {
                currentMissileSpeed = baseMissileSpeed + 3f;
            }
            else
            {
                currentMissileSpeed = baseMissileSpeed + 4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.tag);

        // Check for collision with player's missile
        if (collision.CompareTag("missile"))
        {
            // Instantiate the impact particle effect at the collision point
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }

            // Destroy both the bazooka and the player's missile
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        // Check for collision with the player
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Bazooka collided with the player.");

            // Reduce the player's health through GameManager and destroy the bazooka
            if (GameManager.instance != null)
            {
                GameManager.instance.DamagePlayer();
                GameManager.instance.PlaySound(GameManager.instance.heartLostSound); // Play heart lost sound
            }

            // Instantiate impact effect if available
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }
}
