using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveSpeed = 5.5f;

    [Tooltip("地面按下方向键时的加速速度")]
    public float groundAcceleration = 35f;

    [Tooltip("地面松开方向键后的减速速度")]
    public float groundDeceleration = 45f;

    [Tooltip("空中移动控制能力")]
    public float airAcceleration = 12f;

    [Tooltip("空中松开方向键后的减速速度")]
    public float airDeceleration = 8f;

    [Header("Jump Settings")]
    public float jumpHeight = 1.6f;
    public float gravity = -24f;

    [Tooltip("下落时的重力倍率，数值越大下落越快")]
    public float fallMultiplier = 1.45f;

    [Tooltip("提前松开空格时的重力倍率，用来做小跳")]
    public float lowJumpMultiplier = 2.0f;

    [Tooltip("贴地速度，防止 CharacterController 接地不稳定")]
    public float groundedStickVelocity = -2f;

    [Header("Jump Feel Settings")]
    public float coyoteTime = 0.12f;
    public float jumpBufferTime = 0.12f;

    [Header("Area Limit")]
    public float minX = -4.5f;
    public float maxX = 4.5f;
    public float minZ = -2.8f;
    public float maxZ = 2.8f;

    [Header("Control")]
    public bool canMove = true;

    private CharacterController controller;

    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    private float coyoteTimer;
    private float jumpBufferTimer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        HandleMove();
        HandleJumpAndGravity();
        ClampPlayerPosition();
    }

    private void HandleMove()
    {
        bool isGrounded = controller.isGrounded;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = transform.right * inputX + transform.forward * inputZ;
        inputDirection.y = 0f;

        if (inputDirection.magnitude > 1f)
        {
            inputDirection.Normalize();
        }

        Vector3 targetVelocity = inputDirection * moveSpeed;

        bool hasInput = inputDirection.sqrMagnitude > 0.01f;

        float accel;

        if (isGrounded)
        {
            accel = hasInput ? groundAcceleration : groundDeceleration;
        }
        else
        {
            accel = hasInput ? airAcceleration : airDeceleration;
        }

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            accel * Time.deltaTime
        );

        controller.Move(horizontalVelocity * Time.deltaTime);
    }

    private void HandleJumpAndGravity()
    {
        bool isGrounded = controller.isGrounded;

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;

            if (verticalVelocity < 0f)
            {
                verticalVelocity = groundedStickVelocity;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0f && coyoteTimer > 0f)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            jumpBufferTimer = 0f;
            coyoteTimer = 0f;
        }

        float gravityMultiplier = 1f;

        if (verticalVelocity < 0f)
        {
            gravityMultiplier = fallMultiplier;
        }
        else if (verticalVelocity > 0f && !Input.GetButton("Jump"))
        {
            gravityMultiplier = lowJumpMultiplier;
        }

        verticalVelocity += gravity * gravityMultiplier * Time.deltaTime;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void ClampPlayerPosition()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        transform.position = position;
    }

    public void SetMoveEnabled(bool enabled)
    {
        canMove = enabled;

        if (!enabled)
        {
            horizontalVelocity = Vector3.zero;
            verticalVelocity = 0f;
        }
    }

    public void ResetPlayer(Vector3 startPosition)
    {
        controller.enabled = false;

        transform.position = startPosition;
        horizontalVelocity = Vector3.zero;
        verticalVelocity = 0f;
        jumpBufferTimer = 0f;
        coyoteTimer = 0f;

        controller.enabled = true;
    }
}