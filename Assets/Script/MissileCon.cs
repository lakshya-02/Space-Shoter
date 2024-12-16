using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissileCon : MonoBehaviour
{
    public float missileSpeed = 1.0f;
    public GameObject impactEffect; // Particle effect for collision

    private void Start()
    {
        // Ignore missile collisions with other missiles
        Collider2D missileCollider = GetComponent<Collider2D>();
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("missile");
        foreach (GameObject missile in missiles)
        {
            Collider2D otherMissileCollider = missile.GetComponent<Collider2D>();
            if (otherMissileCollider != null && otherMissileCollider != missileCollider)
            {
                Physics2D.IgnoreCollision(missileCollider, otherMissileCollider);
            }
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collision with the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

        // Handle the collision between player missile and bazooka missile
        if (collision.gameObject.CompareTag("bazooka") && this.CompareTag("missile"))
        {
            // Trigger effect (like explosion) on missile-bazooka collision
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }

            // Destroy both missiles
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            // Play sound or visual effect if needed
            if (GameManager.instance != null)
            {
                GameManager.instance.PlaySound(GameManager.instance.explosionSound);
            }

            return;
        }

        // Handle collision with other objects like enemies, asteroids, or baz
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("baz"))
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f);

            if (GameManager.instance != null)
            {
                int points = collision.gameObject.CompareTag("Enemy") ? 20 : collision.gameObject.CompareTag("Asteroid") ? 15 : 30;
                GameManager.instance.AddScore(points); // Different points for enemy, asteroid, and baz
                GameManager.instance.PlaySound(GameManager.instance.explosionSound); // Play explosion sound
            }

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        // Ignore collision with the heart
        if (collision.gameObject.CompareTag("Heart"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
    }
}
