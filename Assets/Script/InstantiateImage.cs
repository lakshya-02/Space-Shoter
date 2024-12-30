using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateImage : MonoBehaviour
{
    public GameObject[] imagePrefabs; // Array of image prefabs to instantiate
    public Transform canvasTransform; // Reference to the Canvas transform
    public float spawnInterval = 0.5f; // Interval between spawns
    public float moveSpeed = 50f; // Speed of the image movement
    public float centerAvoidanceWidth = 200f; // Width of the center area to avoid

    private void Start()
    {
        // Start spawning images at regular intervals
        InvokeRepeating("SpawnImage", 1f, spawnInterval);
    }

    private void SpawnImage()
    {
        // Select a random image prefab from the array
        int randomIndex = Random.Range(0, imagePrefabs.Length);
        GameObject imagePrefab = imagePrefabs[randomIndex];

        // Instantiate the image prefab
        GameObject imageObj = Instantiate(imagePrefab, canvasTransform);

        // Set a random position within the canvas boundaries, avoiding the center area
        RectTransform rectTransform = imageObj.GetComponent<RectTransform>();
        float canvasWidth = canvasTransform.GetComponent<RectTransform>().rect.width;
        float canvasHeight = canvasTransform.GetComponent<RectTransform>().rect.height;
        float xPos = Random.Range(-canvasWidth / 2 + centerAvoidanceWidth / 2, canvasWidth / 2 - centerAvoidanceWidth / 2);
        rectTransform.anchoredPosition = new Vector2(xPos, canvasHeight / 2);

        // Add the MoveImage component to the instantiated image
        MoveImage moveImage = imageObj.AddComponent<MoveImage>();
        moveImage.moveSpeed = moveSpeed;
        moveImage.canvasHeight = canvasHeight;

        // Set movement type: vertical for odd-indexed images, slant for even-indexed images
        moveImage.isSlantMovement = (randomIndex % 2 == 0);
    }
}

public class MoveImage : MonoBehaviour
{
    public float moveSpeed = 50f;
    public bool isSlantMovement = false; // Flag to determine movement type
    public float canvasHeight; // Height of the canvas

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Move the image downwards or slant based on the flag
        if (isSlantMovement)
        {
            rectTransform.anchoredPosition += new Vector2(-moveSpeed, -moveSpeed) * Time.deltaTime;
        }
        else
        {
            rectTransform.anchoredPosition += Vector2.down * moveSpeed * Time.deltaTime;
        }

        // Destroy the image if it moves out of the canvas boundaries
        if (rectTransform.anchoredPosition.y < -canvasHeight / 2)
        {
            Destroy(gameObject);
        }
    }
}