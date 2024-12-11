using Unity.VisualScripting;
using UnityEngine;

public class FruitNinja : Enemy
{
    [SerializeField] private float distThreshold = 30;
    public Transform playerTransform;
    [SerializeField] private float projectileFireRate = 2;
    private float timeSinceLastFire = 0;
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        if (distThreshold <= 0)
            distThreshold = 30;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        sr.flipX = (transform.position.x > playerTransform.position.x);
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance <= distThreshold)
        {
            if (curPlayingClips[0].clip.name.Contains("Idle"))
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }

    }
}       
          
    



