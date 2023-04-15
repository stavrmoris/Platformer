using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Скорость")]
    public float moveSpeed;
    [Header("Сила прыжка")]
    public float jumpForce;
    [Header("Скорость сползания со стены")]
    public float wallSlidingSpeed;
    [Header("Сила прыжка от стены")]
    public float pushOutFromWallForce;
    public float wallJumpTime;
    public int jumpsAmount;
    public int jumpsLeft;
    private Animator anim;

    public bool IsJumping = false;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    float moveInput;
    float moveY;
    public bool isGrounded;

    Rigidbody2D rb2d;
    float scaleX;

    public bool touchingWall;
    public bool wallJumping;
    public Transform WallCheck;
    public LayerMask WallLayer;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        Jump();
        WallJump();

        if (moveInput != 0 && !touchingWall && !IsJumping)
            anim.SetBool("run", true);
        else if(moveInput == 0 && !IsJumping)
            anim.SetBool("run", false);

        if(rb2d.velocity.y < 0)
        {
            anim.SetBool("fall", true);
            IsJumping = true;
        }
        else
        {
            anim.SetBool("fall", false);
            IsJumping = false;
        }

        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!wallJumping)
        {
            Move();
        }
        CheckIfTouchingWall();
        WallSlide();
        //CheckIfgrounded();
    }

    public void Move()
    {
        Flip();
        rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
    }

    public void Flip()
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        if (moveInput < 0)
        {
            transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z);
        }

    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            CheckIfgrounded();
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                anim.SetTrigger("jump");
                IsJumping = true;
            }
        }
    }

    public void CheckIfgrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
        ResetJumps();
    }

    public void ResetJumps()
    {
        if (isGrounded)
        {
            jumpsLeft = jumpsAmount;
        }
    }

    public void CheckIfTouchingWall()
    {
        touchingWall = Physics2D.OverlapCircle(WallCheck.position, WallCheck.GetComponent<CircleCollider2D>().radius, WallLayer);
    }

    public void WallSlide()
    {
        if (touchingWall)
        {
            if (moveInput == 0)
            {
                touchingWall = false;
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

        }
    }

    public void NotWallJumping()
    {
        wallJumping = false;
    }

    public void WallJump()
    {
        if (touchingWall && !isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                wallJumping = true;
                rb2d.velocity = new Vector2(-moveInput * pushOutFromWallForce, jumpForce);
                Invoke("NotWallJumping", wallJumpTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && touchingWall)
        {
            jumpsLeft = 1;
        }
    }
}