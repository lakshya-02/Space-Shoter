using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    [SerializeField] public float fallSpeed = 2f;
    [SerializeField] private float minFallSpeed = 3f;
    [SerializeField] private float maxFallSpeed = 7f;

    private void Start()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed); // Adjust the range as needed
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")||collision.gameObject.CompareTag("Asteroid")||collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("bazooka")||collision.gameObject.CompareTag("Heart")||collision.gameObject.CompareTag("baz")||collision.gameObject.CompareTag("missile"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        } 
    }
}
