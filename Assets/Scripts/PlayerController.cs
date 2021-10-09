using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementInputDir;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump;

    private int amountofJumpsLeft;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D collider;

    public float movementSpeed = 10.0f;
    public float jumpForce = 40.0f;
    public float groundCheckRadius;

    public int amountofJumps = 2;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        amountofJumpsLeft = amountofJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
        anim.SetFloat("verticalSpeed", rb.velocity.y);
    }

    private void CheckInput()
    {
        movementInputDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(PowerUP(1));
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PowerUP(2));
        }

        if(Input.GetKey(KeyCode.C) && isGrounded && rb.velocity.x!=0)
        {     
            StartCoroutine(Roll());
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius , whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0)
        {
            anim.SetBool("isJumping", false);
            amountofJumpsLeft = amountofJumps;
        }
        
        if(amountofJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDir < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDir >0)
        {
            Flip();
        }

        if(rb.velocity.x!=0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void UpdateAnimation()
    {
        anim.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        if(canJump)
        {   
            anim.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountofJumpsLeft--;
        }
    }


    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDir, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    IEnumerator Roll()
    {
        anim.SetBool("isRolling", true);
        collider.size = new Vector2(collider.size.x, 1.2f);
        collider.offset = new Vector2(collider.offset.x, -3f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("isRolling", false);
        collider.size = new Vector2(collider.size.x, 2.4f);
        collider.offset = new Vector2(collider.offset.x, -2.4f);
    }
    IEnumerator PowerUP(int ch)
    {
        anim.SetInteger("powerUp", ch);
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("powerUp", 0);

    }
}
