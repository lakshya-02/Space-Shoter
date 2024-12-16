using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float baseSpeed = 5f; // Base speed
    private float currentSpeed;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        UpdateSpeed();
    }

    private void Update()
    {
        UpdateSpeed();
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        if (gameManager != null)
        {
            int score = gameManager.GetCurrentScore();

             if (score <= 0)
            {
                currentSpeed = baseSpeed;
            }
            else if (score <= 50)
            {
                currentSpeed = baseSpeed + 0.5f;
            }
            else if (score <= 150)
            {
                currentSpeed = baseSpeed + 1f;
            }
            else if (score <= 275)
            {
                currentSpeed = baseSpeed + 1.5f;
            }
            else if (score <= 350)
            {
                currentSpeed = baseSpeed + 2f;
            }
            else if (score <= 425)
            {
                currentSpeed = baseSpeed + 2.75f;
            }
            else if (score <= 500)
            {
                currentSpeed = baseSpeed + 3.25f;
            }
            else if (score <= 585)
            {
                currentSpeed = baseSpeed + 3.75f;
            }
            else if (score <= 625)
            {
                currentSpeed = baseSpeed + 4.25f;
            }
            else if (score <= 725)
            {
                currentSpeed = baseSpeed + 5.75f;
            }
            else if (score <= 850)
            {
                currentSpeed = baseSpeed + 6.5f;
            }
            else
            {
                currentSpeed = baseSpeed + 7.5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with "Wall" tagged object, allowing movement through the top boundary
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("baz"))
        {
            return;
        }
    }
}
