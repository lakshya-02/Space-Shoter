using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    public float leftBoundary = -7f;
    public float rightBoundary = 7f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime = 5f;
    public Transform muzzleSpawnPos;
    private bool doubleShootActive = false;

    private void Start()
    {
        // Prevent player from colliding with missiles
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Missile"));
        // Prevent missiles from colliding with hearts
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Missile"), LayerMask.NameToLayer("Heart"));
    
    }

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
        CheckPauseInput();
        HandleWrapAround();
    }

    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void HandleWrapAround()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x > rightBoundary)
        {
            newPosition.x = leftBoundary;
        }
        else if (newPosition.x < leftBoundary)
        {
            newPosition.x = rightBoundary;
        }

        // No upper or lower boundary checks, so no modifications here
         
        transform.position = newPosition;
    }

   private void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (doubleShootActive)
            {
                SpawnMissile();
                SpawnMissile();
                SpawnMuzzleEffect();
                SpawnMuzzleEffect();
            }
            else
            {
                SpawnMissile();
                SpawnMuzzleEffect();
            }
            GameManager.instance.PlaySound(GameManager.instance.fireSound);
        }
    }
    void SpawnMissile()
    {
        GameObject gm = Instantiate(missile, missileSpawnPosition);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    void SpawnMuzzleEffect()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzlee, muzzleSpawnPos);
        muzzle.transform.SetParent(null);
        Destroy(muzzle, destroyTime);
    }

    void CheckPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(3);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("baz") || collision.gameObject.CompareTag("bazooka"))
        {
            GameManager.instance.DamagePlayer();
            transform.position = new Vector3(0, -4f, 0); // Reset player position after taking damage
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Heart"))
        {
            GameManager.instance.RestoreHealth();
            transform.rotation = Quaternion.identity; 
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("light"))
        {
            StartCoroutine(DoubleShootPowerUp());
            GameManager.instance.PlaySound(GameManager.instance.lightningCollectSound);
            transform.rotation = Quaternion.identity; // Reset player rotation after collision
            Destroy(collision.gameObject);
        }
    }
     private IEnumerator DoubleShootPowerUp()
    {
        doubleShootActive = true;
        yield return new WaitForSeconds(5f);
        doubleShootActive = false;
    }
}
