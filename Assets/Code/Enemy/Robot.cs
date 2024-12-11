using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Robot : Enemy
{
    private Rigidbody2D rb;

    [SerializeField] private float xVel;
    [SerializeField] private int damage = 1;

    public override void Start()
    {
        {
            if (damage <= 0) damage = 1;
            //Destroy(gameObject);
        }

        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVel <= 0) xVel = 6;
    }

    private void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curPlayingClips[0].clip.name.Contains("Walk"))
        {
            rb.velocity = (sr.flipX) ? new Vector2(-xVel, rb.velocity.y) : new Vector2(xVel, rb.velocity.y);
        }
    }

    

    public override void TakeDamage(int damageValue)
    {
        if (damageValue >= 9999)
        {
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }

        base.TakeDamage(damageValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("TurnAround");
            sr.flipX = !sr.flipX;
        }
    }
}




//public class Projectile : MonoBehaviour
//{
    

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
       

//        if (collision.gameObject.CompareTag("Enemy") && CompareTag("PlayerProjectile"))
//        {
//            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
//            Destroy(gameObject);
//        }

//        if (collision.gameObject.CompareTag("Player") && CompareTag("EnemyProjectile"))
//        {
//            GameManager.Instance.lives--;
//            Destroy(gameObject);
//        }
//    }
//}
