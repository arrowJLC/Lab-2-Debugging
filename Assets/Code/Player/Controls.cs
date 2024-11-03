using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class NewBehaviourScript : MonoBehaviour
{

    [Range(5, 25)]
    public float speed = 7.0f;
    [Range(3, 25)]
    public float jumpForce = 7.0f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;


    [Range(0.8f, 5f)]
    public float groundCheckRadius = 0.8f;
    public LayerMask isGroundLayer;
    bool isGrounded = false;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        float hinput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hinput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (hinput != 0) sr.flipX = (hinput < 0);
        {
            anim.SetFloat("playSpeed", Mathf.Abs(hinput));
            anim.SetBool("isGrounded", isGrounded);
        }

        void CheckIsGrounded()
        {
            if (!isGrounded)
            {
                if (rb.velocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            }
            else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }

    }
}
