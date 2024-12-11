using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public Vector2 initialShotVelocity = Vector2.zero;
    
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile EnemyProjectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initialShotVelocity == Vector2.zero)
        {
            initialShotVelocity.x = 10.0f;
        }
       

        if (!spawnPointRight || !spawnPointLeft || !EnemyProjectilePrefab)
            Debug.Log($"for {gameObject.name}");
    }

    public void Fire()
    {
        //if (sr.flipX)
        //{
        //    Projectile curProjectile = Instantiate(EnemyProjectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
        //    curProjectile.SetVelocity(initialShotVelocity);
        //}

        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(EnemyProjectilePrefab, spawnPointRight.position, spawnPointRight.rotation);

            curProjectile.SetVelocity(new Vector2(initialShotVelocity.x, initialShotVelocity.y));
        }

        else
        {
            Projectile curProjectile = Instantiate(EnemyProjectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(new Vector2(-initialShotVelocity.x, initialShotVelocity.y));
        }

    }
}
