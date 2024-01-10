using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private GameManager gameManager;
    private float horizontal;
    
    private bool isFacingRight = true;
    private float hangCounter;
    private float jumpBufferCount;
    public Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("General Movement")]
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float maxYVelocity = 20f;
    public int maxJumps = 2;
    private int jumpsLeft;
    public float hangTime = .2f;
    public float jumpBuggerLenght = .1f;
    public bool canGlide = false;
    private bool isHangGliding = false;
    public float glideGrav = 0.1f;

    public bool canPaddoJump = false;
    public bool isSaus;
    [HideInInspector] public GameObject jumpingPaddo;

    [Header("Wall Jumping")]
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    public bool canWallJump = false;
    [HideInInspector]
    public bool isWallSliding;
    private bool isWallJumping;
    private bool isInAir;
    [HideInInspector]
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;

    [SerializeField] private float wallSlidingSpeed = 2f;

    [Header("Camera")]
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;
    public List<GameObject> paddos = new List<GameObject>();

    private void Start()
    {
        jumpsLeft = maxJumps;
        gameManager = GameManager.instance;
    }

    void Update()
    {
        //Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (jumpsLeft > 0)
        {
            if (jumpBufferCount >= 0 && hangCounter > 0f)
            {
                anim.SetBool("IsJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpBufferCount = 0;
                jumpsLeft -= 1;
                Debug.Log("Jump 1");
            }
            else if (maxJumps > 1 && Input.GetButtonDown("Jump") && !IsGrounded())
            {
                anim.SetBool("IsJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                jumpsLeft -= 1;
                Debug.Log("Jump 2");
            }

        }
        //Small jump
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && jumpsLeft >= 0)
        {
            //anim.SetBool("IsJumping", false);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpBufferCount = 0;
            Debug.Log("Jump 3");
        }

        if (IsGrounded() && isInAir && !isSaus)
        {
            isInAir = false;
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
        }
        else if (!IsGrounded() && !isInAir && !isSaus)
        {
            isInAir = true;
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.NoFriction;
        }

        //Hantime
        if (IsGrounded() && rb.velocity.y <= 2)
        {
            anim.SetBool("IsJumping", false);
            hangCounter = hangTime;
            jumpsLeft = maxJumps;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }


        if(hangCounter > 0f)
        {
            isHangGliding = false;
        }
        else
        {
            isHangGliding = true;
        }
        //JumpBuffer
        if (Input.GetButtonDown("Jump"))
        {
            if (!isSaus)
            {
                gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.NoFriction;
            }
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

        if (canGlide && isHangGliding)
        {
            Glide();
        }
        else
        {
            rb.gravityScale = 3;
            anim.SetBool("IsFloating", false);
        }

        if (!isWallJumping)
        {
            Flip();
        }

        if (canPaddoJump)
        {
            DoPaddoJump();
        }

        //Move camera point
        //if (Input.GetAxisRaw("Horizontal") != 0)
        //{
        //    camTarget.localPosition = new Vector3(Mathf.Lerp(camTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), camTarget.localPosition.y, camTarget.localPosition.z);
        //}
    }

    private void FixedUpdate()
    {
        if(horizontal != 0)
        {
            anim.SetFloat("Speed", 1);
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.NoFriction;
        }
        else
        {
            anim.SetFloat("Speed", 0);
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
        }

        if (rb.velocity.y > maxYVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxYVelocity);
        }

        if (IsGrounded() && horizontal == 0 || isSaus)
        {
            return;
        }

        if (!isWallJumping)
        {
            //movement :)
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void DoPaddoJump()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            Vector3 currentLocation = new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            StartCoroutine(SpawnPaddo(currentLocation, 0.5f));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray;
            float distance = 100;
            ray = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
            Debug.Log(ray + " is ray");
            if (ray)
            {
                Vector3 spawnLocation = new Vector3(ray.point.x, ray.point.y + 0.5f);
                StartCoroutine(SpawnPaddo(spawnLocation, 0f));
            }
            //rb.velocity = new Vector2(0, -10);
        }
    }

    private IEnumerator SpawnPaddo(Vector3 pos, float time)
    {
        yield return new WaitForSeconds(time);
        if(paddos.Count > 0 && paddos[0] != null)
        {
            paddos[0].GetComponent<Paddo>().DespawnPaddo(); 
            //Destroy(paddos[0]);
            paddos.Clear();
            Debug.Log("Gebeurt dit?");
        }

        Debug.Log(paddos.Count);
        
        GameObject tmpPaddo = Instantiate(jumpingPaddo, pos, Quaternion.identity);
        paddos.Add(tmpPaddo);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            if (!isWallSliding)
            {
                isWallSliding = true;
                anim.SetBool("IsWallSliding", true);
                //gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.NoFriction;
            }
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            if (isWallSliding)
            {
                isWallSliding = false;
                anim.SetBool("IsWallSliding", false);
                //gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
            }
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

    private void Glide()
    {
        
        if(!IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            anim.SetBool("IsFloating", true);

            Debug.Log("Glide");
            rb.gravityScale = glideGrav;
        }

        if (IsGrounded())
        {
            rb.gravityScale = 3f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 3f;
        }

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
