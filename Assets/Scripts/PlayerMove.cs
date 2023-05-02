using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private PlayerStats playerStats;
    private float horizontal;
    
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;

    private bool isTalking;
    private bool isCanJumping;
    [SerializeField] private bool isJumping = false;
    
    private bool isdoubleJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        bool isGround = IsGrounded();
        if (isDashing || isTalking)
        {
            return;
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGround)
        {
            coyoteTimeCounter = coyoteTime;
            if(rb.velocity.y <0)
            {
                isJumping = false;
                isdoubleJumping = false;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(isJumping && !isdoubleJumping && playerStats.GetisDoubleJumpOpened())
            {
                isdoubleJumping = true;
            }
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash && playerStats.GetisDashOpened())
        {
            StartCoroutine(Dash());
        }
        Flip();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * playerStats.GetMoveSpeed(), rb.velocity.y);

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.AddForce(new Vector2(0f, playerStats.GetJumpPower()), ForceMode2D.Impulse);

            jumpBufferCounter = 0f;

            isJumping = true;
        }
        if (isJumping && isdoubleJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, playerStats.GetJumpPower()), ForceMode2D.Impulse);
        }
        //if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        //    coyoteTimeCounter = 0f;
        //}
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void isTalkingTrue()
    {
        isTalking = true;
    }
    public void isTalkingFalse()
    {
        isTalking = false;
    }
    private void Flip() // 뒤보는지 앞보는지 체크
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * playerStats.GetdashPower(), 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(playerStats.GetdashingTime());

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        for (int i = 0; i < 100; i++)
        {
            GameManager.instance.ui.GetDashImage().fillAmount = (float)i / playerStats.GetdashingCooldown() / 100;
            yield return new WaitForSeconds((float)playerStats.GetdashingCooldown() / 100);
        }

        yield return new WaitForEndOfFrame();
        GameManager.instance.ui.GetDashImage().fillAmount = 1;
        canDash = true;
    }
}
