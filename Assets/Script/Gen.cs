using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{
    public float baseSpeed = 3f; // Base speed of the GenShip
    private float currentSpeed;
    private bool movingLeft = true; // Direction flag

    [SerializeField] private int totalHearts = 15; // Total hearts for the GenShip
    private int currentHearts;

    [SerializeField] private GameObject genMissilePrefab; // Missile prefab for firing
    [SerializeField] private Transform missileSpawnPointLeft; // Spawn point for left side
    [SerializeField] private Transform missileSpawnPointRight; // Spawn point for right side

    [Header("Missile Settings")]
    [SerializeField] private float missileSpawnDelay = 2f; // Delay between missile spawns

    void Start()
    {
        currentHearts = totalHearts;
        currentSpeed = baseSpeed;

        InvokeRepeating("FireMissiles", missileSpawnDelay, missileSpawnDelay);
    }

    void Update()
    {
        Move(); // Move the GenShip
    }

    void Move()
    {
        if (movingLeft)
        {
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
            if (transform.position.x <= -6.5f) // Change direction at left boundary
            {
                movingLeft = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
            if (transform.position.x >= 6.5f) // Change direction at right boundary
            {
                movingLeft = true;
            }
        }
    }

    public void LoseHeart()
    {
        currentHearts--;

        if (currentHearts <= 0)
        {
            CancelInvoke("FireMissiles"); // Stop firing missiles when destroyed
            Destroy(gameObject);
        }
    }

    void FireMissiles()
    {
        Instantiate(genMissilePrefab, missileSpawnPointLeft.position, Quaternion.identity);
        Instantiate(genMissilePrefab, missileSpawnPointRight.position, Quaternion.identity);
    }
}