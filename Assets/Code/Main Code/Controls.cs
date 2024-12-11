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

    [Range(5, 25)]
    public float speed = 7.0f;
    [Range(3, 25)]
    public float jumpForce = 7.0f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;

    public Transform turretTransform;

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
                if (rb.velocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
                rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
                if (Input.GetButtonDown("Jump") && isGrounded && isGrounded && rb.velocity.y <= 0)
                {
                    if (rb.velocity.y < 1)
                    {
                        canJump();
                    }
                }
            }
            else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }

        

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
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }
}

      

