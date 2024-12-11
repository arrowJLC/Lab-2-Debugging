using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]


public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    [SerializeField, Range(1, 200)] private float lifetime;


    // Start is called before the first frame update
    void Start()
    {
        if (damage <= 0) damage = 1;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }
}
