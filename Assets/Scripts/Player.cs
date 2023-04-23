using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 8f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private Image dashSkill;

    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal"); // horizontal

        if (IsGrounded()) // return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        {
            coyoteTimeCounter = coyoteTime; // 땅에 있으면 0.2로 초기화
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // 땅에 없으면 카운터가 시작되며, 카운터가 0이하가 되면 점프가 불가능하게 바뀐다.
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 점프키 클릭
        {
            jumpBufferCounter = jumpBufferTime; // 버퍼시간을 0.2로 초기화
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime; // 점프키 클릭하면, 점프버퍼타임이 줄어든다.
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip(); // 보는 방향을 바꾸는 함수
    }

    private void FixedUpdate() // velocity 뿐만 아니라 모든 물리는 FixedUpdate()에서!!!!!!!
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // 이동

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping) // 코요테가 0이하면 또는 점프 버퍼타임이 0이하면또는 점프중이면 점프 못함.
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // velocity.y 를 0으로 초기화
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // 점프 파워 만큼 점프

            jumpBufferCounter = 0f; // 0으로 초기화해서 혹시 모를 버그 없애기

            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f) // 점프키를 손에서 땟을때, 또는 velocity가 위로 올라가고 있을때,
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); // 0.5f를 곱해 0이 될때까지 y를 줄임.

            coyoteTimeCounter = 0f; // 0으로 초기화해서 혹시 모를 버그 없애기
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // 땅인지 아닌지 체크
    }
    private void Flip() // 뒤보는지 앞보는지 체크
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f; // 뒤면 몸통 반대로
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        for (int i = 0; i < 100; i++)
        {
            dashSkill.fillAmount = (float)i / dashingCooldown / 100;
            yield return new WaitForSeconds((float)dashingCooldown / 100);
        }

        yield return new WaitForEndOfFrame();
        dashSkill.fillAmount = 1;
        canDash = true;
    }
}
