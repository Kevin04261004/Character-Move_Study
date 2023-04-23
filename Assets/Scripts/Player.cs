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
            coyoteTimeCounter = coyoteTime; // ���� ������ 0.2�� �ʱ�ȭ
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // ���� ������ ī���Ͱ� ���۵Ǹ�, ī���Ͱ� 0���ϰ� �Ǹ� ������ �Ұ����ϰ� �ٲ��.
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // ����Ű Ŭ��
        {
            jumpBufferCounter = jumpBufferTime; // ���۽ð��� 0.2�� �ʱ�ȭ
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime; // ����Ű Ŭ���ϸ�, ��������Ÿ���� �پ���.
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip(); // ���� ������ �ٲٴ� �Լ�
    }

    private void FixedUpdate() // velocity �Ӹ� �ƴ϶� ��� ������ FixedUpdate()����!!!!!!!
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // �̵�

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping) // �ڿ��װ� 0���ϸ� �Ǵ� ���� ����Ÿ���� 0���ϸ�Ǵ� �������̸� ���� ����.
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // velocity.y �� 0���� �ʱ�ȭ
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // ���� �Ŀ� ��ŭ ����

            jumpBufferCounter = 0f; // 0���� �ʱ�ȭ�ؼ� Ȥ�� �� ���� ���ֱ�

            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f) // ����Ű�� �տ��� ������, �Ǵ� velocity�� ���� �ö󰡰� ������,
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); // 0.5f�� ���� 0�� �ɶ����� y�� ����.

            coyoteTimeCounter = 0f; // 0���� �ʱ�ȭ�ؼ� Ȥ�� �� ���� ���ֱ�
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // ������ �ƴ��� üũ
    }
    private void Flip() // �ں����� �պ����� üũ
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f; // �ڸ� ���� �ݴ��
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
