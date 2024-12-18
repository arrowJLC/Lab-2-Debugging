using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck))]
public class NewBehaviourScript : MonoBehaviour
{

    public Transform WinCondiotionsTransform;
    public AudioClip jumpSound;
    public AudioClip shrinkSound;

    [Range(5, 25)]
    public float speed = 7.0f;
    [Range(3, 25)]
    public float jumpForce = 7.0f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;
    AudioSource audioSource;

    public Transform turretTransform;
    [SerializeField] private AudioClip deathSound;

    [Range(0.8f, 5f)]
    public float groundCheckRadius = 0.8f;
    public float scaleSet = 0.50f;
    public float ChangeSclae= 1;
    public LayerMask isGroundLayer;
    bool isGrounded = false;
    //public bool jumpCancelled = false;
    float calculatedJumpForce;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
        audioSource = GetComponent<AudioSource>();

        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //if (isGrounded) jumpCancelled = false;

        float hinput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hinput * speed, rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);

        if (hinput != 0) sr.flipX = (hinput < 0);
        {
            anim.SetFloat("playSpeed 0", Mathf.Abs(hinput));
            anim.SetBool("isGrounded", isGrounded);
        }
        CheckIsGrounded();

        void canJump()
        {
            //if (Input.GetButtonUp("Jump"))
            //{
            //    if (rb.velocity.y < -10) return;
            //    jumpCancelled = true;
            //}

            //if (Input.GetButtonDown("Jump") && isGrounded && rb.velocity.y <= 0)
            //{
               rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            //}
            CheckIsGrounded();
        }
        
        void CheckIsGrounded()
        {
            if (isGrounded)
            {
                Debug.Log($"Ground check working");

                if (rb.velocity.y <= 0)
                    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

                // Play the jump sound when the player jumps (on the ground)
                if (Input.GetButtonDown("Jump") && isGrounded && rb.velocity.y <= 0)
                {
                    if (rb.velocity.y < 1)
                    {
                        // Play the jump sound
                        //audioSource.PlayOneShot(jumpSound);

                        // Make the player jump
                        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            }
        }

        //void CheckIsGrounded()
        //{
        //    if (isGrounded)
        //    {
        //        Debug.Log($"Ground check working");

        //        if (rb.velocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        //        rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
        //        if (Input.GetButtonDown("Jump") && isGrounded && isGrounded && rb.velocity.y <= 0)
        //        {

        //            if (rb.velocity.y < 1)
        //            {
        //                canJump();
        //            }
        //        }
        //    }
        //    else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        //}



        if (!isGrounded)
        {
            Debug.Log($"Ground check not working");
            //jumpCancelled = false;
            //rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
        }

        Vector3 turretPosition = turretTransform.position;

        float distanceToTurret = Vector3.Distance(transform.position, turretPosition);

        if (distanceToTurret < 5f) { }
    }

    public void JumpPowerUp()
    {
        StartCoroutine(JumpHeightChange());
    }
    IEnumerator JumpHeightChange()
    {
        jumpForce *= 2;
        //calculatedJumpForce = Mathf.Sqrt(jumpForce * -2 * Physics2D.gravity.y * rb.gravityScale);

        yield return new WaitForSeconds(5);

        jumpForce /= 2;
        //calculatedJumpForce = Mathf.Sqrt(jumpForce * -2 * Physics2D.gravity.y * rb.gravityScale);
    }


    public void shrinkPowerUp()
    {
        StartCoroutine(ShrinkHeightChange());
    }

    IEnumerator ShrinkHeightChange()
    {

        ChangeScale(scaleSet);
        audioSource.PlayOneShot(shrinkSound);


        yield return new WaitForSeconds(10);

       
        ChangeScale(1);  
    }

    void ChangeScale(float scaleFactor)
    {
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && CompareTag("Player"))
        {
            //audioSource.PlayOneShot(deathSound);

            Destroy(gameObject, deathSound.length);
            SceneManager.LoadScene("Game Over");
        }
    }
}

//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.gameObject.CompareTag("Enemy") && CompareTag("Player"))
//    {
//        // Play the death sound immediately
//        audioSource.PlayOneShot(deathSound);

//        // Start a coroutine to handle the death process, including the scene change
//        StartCoroutine(HandleDeath());
//    }
//}

//IEnumerator HandleDeath()
//{
//    // Wait for the length of the death sound to finish playing
//    yield return new WaitForSeconds(deathSound.length);

//    // Destroy the player after the sound is finished
//    Destroy(gameObject);

//    // Load the "Game Over" scene after the player has been destroyed
//    SceneManager.LoadScene("Game Over");
//}

//private void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.gameObject.CompareTag("Enemy") && CompareTag("Player"))
//    {
//        // Play the death sound
//        audioSource.PlayOneShot(deathSound);

//        // Destroy the player after the death sound has finished playing
//        Destroy(gameObject, deathSound.length);

//        // Start a coroutine to wait for the death sound to finish before loading the game over scene
//        StartCoroutine(LoadGameOverScene(deathSound.length));
//    }
//}

//IEnumerator LoadGameOverScene(float delay)
//{
//    // Wait for the specified time (death sound length)
//    yield return new WaitForSeconds(delay);

//    // Load the "Game Over" scene
//    SceneManager.LoadScene("Game Over");
//}    

