using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introfalling : MonoBehaviour
{
    public static introfalling instance;

    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject bazShipPrefab;

    [Header("Spawn Settings")]
    public float minInstantiateValue;
    public float maxInstantiateValue;
    [SerializeField] private float enemyDestroyTime = 3f;
    [SerializeField] private float asteroidDestroyTime = 4f;
    [SerializeField] private float bazShipDestroyTime = 5f;

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
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(SpawnBazShip());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            InstantiateEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    private void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        enemy.AddComponent<MoveObject>().speed = 2f;
        Destroy(enemy, enemyDestroyTime);
    }

    private IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            InstantiateAsteroid();
            yield return new WaitForSeconds(3f);
        }
    }

    private void InstantiateAsteroid()
    {
        Vector3 asteroidPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject asteroid = Instantiate(asteroidPrefab, asteroidPos, Quaternion.identity);
        asteroid.AddComponent<MoveObject>().speed = 1.5f;
        Destroy(asteroid, asteroidDestroyTime);
    }

    private IEnumerator SpawnBazShip()
    {
        while (true)
        {
            InstantiateBazShip();
            yield return new WaitForSeconds(5f);
        }
    }

    private void InstantiateBazShip()
    {
        Vector3 bazShipPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject bazShip = Instantiate(bazShipPrefab, bazShipPos, Quaternion.identity);
        bazShip.AddComponent<MoveObject>().speed = 2.5f;
        Destroy(bazShip, bazShipDestroyTime);
    }
}

public class MoveObject : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
