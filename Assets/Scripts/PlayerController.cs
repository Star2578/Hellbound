using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isJump = false;

    public float speed = 1f;
    public float jumpPower = 100f;
    public float groundCheckerRAD = 0.05f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Camera.main.transform.position = transform.position + new Vector3(0, 0, -10); //change later
        GroundCheck();
        PlayerVFX();
    }

    void FixedUpdate()
    {
        Controller();
    }

    void Controller()
    {
        Running();

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJump = true;
        }
    }

    void Running()
    {
        float x;

        x = Input.GetAxisRaw("Horizontal");

        if (x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (x < 0)
        {
            sr.flipX = true;
        }
        else if (x > 0)
        {
            sr.flipX = false;
        }

        rb.velocity = new Vector2 (x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void PlayerVFX()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isJump", isJump);
    }

    void GroundCheck()
    {
        isGrounded = false;
        isJump = true;

        Collider2D[] collider = Physics2D.OverlapCircleAll(groundChecker.position, groundCheckerRAD, groundLayer);
        if (collider.Length > 0)
        {
            isGrounded = true;
            isJump = false;
        }
    }
}
