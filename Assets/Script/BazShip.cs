using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazShip : MonoBehaviour
{
    public GameObject bazookaPrefab; // Prefab of the bazooka missile
    public Transform spos; // The position from which the bazooka is fired
    public float minFireInterval = 1f; // Minimum time between shots
    public float maxFireInterval = 3f; // Maximum time between shots
    public float baseSpeed = 3f; // Base speed of the BazShip
    private float currentSpeed;
    private GameManager gameManager;

    private void Start()
    {
        // Rotate the BazShip by 180 degrees initially
        transform.rotation = Quaternion.Euler(0, 0, 180);

        // Get the instance of GameManager
        gameManager = GameManager.instance;

        // Update speed based on the current score
        UpdateSpeed();

        // Start shooting at random intervals
        StartCoroutine(ShootRandomly());
    }

    private void Update()
    {
        // Update speed based on the current score
        UpdateSpeed();

        // Move the BazShip downwards
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        if (gameManager != null)
        {
            int score = gameManager.GetCurrentScore();

            if (score <= 100)
            {
                currentSpeed = baseSpeed;
            }
            else if (score <= 150)
            {
                currentSpeed = baseSpeed + 0.25f;
            }
            else if (score <= 275)
            {
                currentSpeed = baseSpeed + 0.5f;
            }
            else if (score <= 350)
            {
                currentSpeed = baseSpeed + 1.25f;
            }
            else if (score <= 475)
            {
                currentSpeed = baseSpeed + 2f;
            }
            else
            {
                currentSpeed = baseSpeed + 5f;
            }
        }
    }

    private IEnumerator ShootRandomly()
    {
        while (true)
        {
            // Randomly determine the delay before the next shot
            float delay = Random.Range(minFireInterval, maxFireInterval);
            yield return new WaitForSeconds(delay);

            // Fire the bazooka missile
            if (bazookaPrefab != null && spos != null)
            {
                GameObject bazookaInstance = Instantiate(bazookaPrefab, spos.position, Quaternion.Euler(0, 0, 180));

                // Ignore collision between the bazooka and the BazShip
                Collider2D bazookaCollider = bazookaInstance.GetComponent<Collider2D>();
                Collider2D bazShipCollider = GetComponent<Collider2D>();

                if (bazookaCollider != null && bazShipCollider != null)
                {
                    Physics2D.IgnoreCollision(bazookaCollider, bazShipCollider);
                }

                Debug.Log("Bazooka missile fired with 180-degree rotation and collision ignored with BazShip.");
            }
        }
    }
}
