using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;

    private bool isFacingRight = true;
    private float hangCounter;
    private float jumpBufferCount;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("General Movement")]
    public float speed = 8f;
    public float jumpingPower = 16f;
    public int maxJumps = 2;
    private int jumpsLeft;
    public float hangTime = .2f;
    public float jumpBuggerLenght = .1f;

    [Header("Wall Jumping")]
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [HideInInspector] public bool canWallJump = false;
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;

    [SerializeField] private float wallSlidingSpeed = 2f;

    [Header("Camera")]
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;

    private void Start()
    {
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (jumpsLeft > 0)
        {

            if (jumpBufferCount >= 0 && hangCounter > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpBufferCount = 0;
                jumpsLeft -= 1;
                Debug.Log("Jump 1");
            }
            else if (maxJumps > 1 && Input.GetButtonDown("Jump") && !IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsLeft -= 1;
                Debug.Log("Jump 2");
            }

        }
        //Small jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && jumpsLeft >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpBufferCount = 0;
            Debug.Log("Jump 3");
        }



        //Hangtime
        if (IsGrounded() && rb.velocity.y == 0)
        {
            hangCounter = hangTime;
            jumpsLeft = maxJumps;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        //JumpBuffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBuggerLenght;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (canWallJump)
        {
            WallSlide();
            WallJump();
        }

        if (!isWallJumping)
        {
            Flip();
        }

        //Move camera point
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding == true)
        {
            isWallJumping = false;
            if (transform.rotation.y >= 0)
            {
                wallJumpingDirection = -1;
            }

            if (transform.rotation.y <= 0)
            {
                wallJumpingDirection = 1;
            }
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0;
            if (wallJumpingDirection == 1)
            {
                wallJumpingDirection = -1;
            }
            else
            {
                wallJumpingDirection = 1;
            }
            if (transform.rotation.y == 0)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
            else
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            transform.Rotate(0f, 180f, 0f);
            aheadAmount = -aheadAmount;
        }
    }
}
