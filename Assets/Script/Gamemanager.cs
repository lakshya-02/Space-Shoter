using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health System")]
    public Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    private int currentHealth = 3;
    private const int maxHealth = 3;

    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject bazShipPrefab;
    public GameObject heartPrefab; // Added reference to heart prefab

    [Header("Spawn Settings")]
    public float minInstantiateValue = -7f;
    public float maxInstantiateValue = 7f;
    [SerializeField] private float enemyDestroyTime = 3f;
    [SerializeField] private float asteroidDestroyTime = 4f;
    [SerializeField] private float bazShipDestroyTime = 5f;
    [SerializeField] private float heartSpawnIntervalMin = 10f;
    [SerializeField] private float heartSpawnIntervalMax = 20f;

    [Header("Score System")]
    [SerializeField] private Text scoreText;
    private int score = 0;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzlee;

    [Header("Audio Settings")]
    public AudioClip fireSound;
    public AudioClip heartLostSound;
    public AudioClip explosionSound;
    public AudioClip playerDeathSound;
    public AudioClip heartCollectSound; // Added heart collect sound
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        UpdateScore(0);
        InvokeRepeating("InstantiateEnemy", 1f, 2f);
        StartCoroutine(SpawnRandomHearts());
    }

    public void DamagePlayer()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHeartUI();
            PlaySound(heartLostSound);

            if (currentHealth == 0)
            {
                PlayerDeath();
            }
        }
    }

    public void RestoreHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHeartUI();
            PlaySound(heartCollectSound); // Play heart collection sound
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }

    private void PlayerDeath()
    {
        GameObject gm = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gm, 2f);

        PlaySound(playerDeathSound);

        // Save current score to PlayerPrefs
        PlayerPrefs.SetInt("CurrentScore", score);

        // Check if the score is a new high score and save it
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(2); // Game over scene
    }

    private void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        Destroy(enemy, enemyDestroyTime);
    }

    private void InstantiateAsteroid()
    {
        if (score >= 50)
        {
            Vector3 asteroidPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
            GameObject asteroid = Instantiate(asteroidPrefab, asteroidPos, Quaternion.identity);
            Destroy(asteroid, asteroidDestroyTime);
        }
    }

    private void InstantiateBazShip()
    {
        if (score >= 100)
        {
            Vector3 bazShipPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
            GameObject bazShip = Instantiate(bazShipPrefab, bazShipPos, Quaternion.identity);
            Destroy(bazShip, bazShipDestroyTime);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(score);

        if (score >= 50 && !IsInvoking("InstantiateAsteroid"))
        {
            InvokeRepeating("InstantiateAsteroid", 1f, 3f);
        }

        if (score >= 100 && !IsInvoking("InstantiateBazShip"))
        {
            InvokeRepeating("InstantiateBazShip", 2f, 5f);
        }
    }

    private void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }

    public int GetCurrentScore()
    {
        return score;
    }

    private IEnumerator SpawnRandomHearts()
    {
        while (true)
        {
            float spawnInterval = Random.Range(heartSpawnIntervalMin, heartSpawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            Vector3 heartPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
            GameObject heart = Instantiate(heartPrefab, heartPos, Quaternion.identity);

            AddHeart heartScript = heart.GetComponent<AddHeart>();
            if (heartScript != null)
            {
                heartScript.fallSpeed = Random.Range(2f, 6f); // Randomize falling speed
            }
        }
    }
}
