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
    public GameObject lightningPrefab;
    public GameObject heartPrefab;
    public GameObject muzzlee;

    [Header("Spawn Settings")]
    public float minInstantiateValue = -7f;
    public float maxInstantiateValue = 7f;
    [SerializeField] private float enemyDestroyTime = 3f;
    [SerializeField] private float asteroidDestroyTime = 4f;
    [SerializeField] private float bazShipDestroyTime = 5f;
    [SerializeField] private float heartSpawnIntervalMin = 10f;
    [SerializeField] private float heartSpawnIntervalMax = 20f;
    [SerializeField] private float lightningSpawnIntervalMin = 15f;
    [SerializeField] private float lightningSpawnIntervalMax = 25f;

    [Header("Score System")]
    [SerializeField] private Text scoreText;
    private int score = 0;

    [Header("Particle Effects")]
    public GameObject explosion;

    [Header("Audio Settings")]
    public AudioClip fireSound;
    public AudioClip heartLostSound;
    public AudioClip explosionSound;
    public AudioClip playerDeathSound;
    public AudioClip heartCollectSound;
    public AudioClip lightningCollectSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateScore(0);

        // Only enemies start spawning by default, comment out to also gate enemies by score if desired
        InvokeRepeating("InstantiateEnemy", 1f, 2f);

        // Hearts and lightning can spawn from the start
        Invoke("SpawnHeart", Random.Range(heartSpawnIntervalMin, heartSpawnIntervalMax));
        Invoke("SpawnLightning", Random.Range(lightningSpawnIntervalMin, lightningSpawnIntervalMax));
    }

    public void DamagePlayer()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHeartUI();
            PlaySound(heartLostSound);

            if (currentHealth == 0) PlayerDeath();
        }
    }

    public void RestoreHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHeartUI();
            PlaySound(heartCollectSound);
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < currentHealth ? fullHeartSprite : emptyHeartSprite;
        }
    }

    private void PlayerDeath()
    {
        GameObject gm = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gm, 2f);

        PlaySound(playerDeathSound);

        PlayerPrefs.SetInt("CurrentScore", score);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore) PlayerPrefs.SetInt("HighScore", score);

        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(2); // Game over scene
    }

    private void InstantiateEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        var obj = Instantiate(enemyPrefab, pos, Quaternion.identity);
        Destroy(obj, enemyDestroyTime);
    }

    private void InstantiateAsteroid()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        var obj = Instantiate(asteroidPrefab, pos, Quaternion.identity);
        Destroy(obj, asteroidDestroyTime);
    }

    private void InstantiateBazShip()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        var obj = Instantiate(bazShipPrefab, pos, Quaternion.identity);
        Destroy(obj, bazShipDestroyTime);
    }

    private void SpawnHeart()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        var obj = Instantiate(heartPrefab, pos, Quaternion.identity);
        Destroy(obj, asteroidDestroyTime);
        Invoke("SpawnHeart", Random.Range(heartSpawnIntervalMin, heartSpawnIntervalMax));
    }

    private void SpawnLightning()
    {
        Vector3 pos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        var obj = Instantiate(lightningPrefab, pos, Quaternion.identity);
        Destroy(obj, asteroidDestroyTime);
        Invoke("SpawnLightning", Random.Range(lightningSpawnIntervalMin, lightningSpawnIntervalMax));
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
        if (scoreText != null) scoreText.text = "Score: " + newScore;
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
}