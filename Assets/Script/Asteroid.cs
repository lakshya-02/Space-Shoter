using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float baseSpeed = 3.5f; // Base speed
    private float currentSpeed;
    public Vector2 movementDirection = new Vector2(-1, -1); // Adjust for diagonal movement
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        UpdateSpeed();
    }

    private void Update()
    {
        UpdateSpeed();
        Vector3 direction = new Vector3(movementDirection.x, movementDirection.y, 0).normalized;
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        if (gameManager != null)
        {
            int score = gameManager.GetCurrentScore();

            if (score <= 75)
            {
                currentSpeed = baseSpeed;
            }
            else if (score <= 150)
            {
                currentSpeed = baseSpeed + 0.5f;
            }
            else if (score <= 250)
            {
                currentSpeed = baseSpeed + 1.25f;
            }
            else if (score <= 375)
            {
                currentSpeed = baseSpeed + 1.75f;
            }
            else if (score <= 450)
            {
                currentSpeed = baseSpeed + 2.5f;
            }
            else if (score <= 525)
            {
                currentSpeed = baseSpeed + 3.75f;
            }
            else if (score <= 600)
            {
                currentSpeed = baseSpeed + 4.25f;
            }
            else if (score <= 685)
            {
                currentSpeed = baseSpeed + 4.75f;
            }
            else if (score <= 725)
            {
                currentSpeed = baseSpeed + 5.25f;
            }
            else if (score <= 825)
            {
                currentSpeed = baseSpeed + 5.75f;
            }
            else if (score <= 950)
            {
                currentSpeed = baseSpeed + 6.5f;
            }
            else
            {
                currentSpeed = baseSpeed + 7.25f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with "Wall" tagged object, allowing movement through the top boundary
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("baz"))
        {
            return;
        }

        // Other collision logic
    }
}
