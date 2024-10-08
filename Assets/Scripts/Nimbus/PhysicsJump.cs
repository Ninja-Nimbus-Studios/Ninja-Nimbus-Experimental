using System;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    // average force should be around 10, 4
    [SerializeField] private float minVerticalForce = 11f;
    [SerializeField] private float maxVerticalForce = 15f;
    [SerializeField] private float minHorizontalForce = 4f;
    [SerializeField] private float maxHorizontalForce = 6.5f;
    [SerializeField] private float maxChargeTime = 1f;

    [Header("Component References")]
    public Rigidbody2D rb;
    private bool isJumping;
    private Camera mainCamera => Camera.main;
    private ViewManager viewManager;
    [SerializeField] CloudPower cloudPower;
    [SerializeField] Nimbus nimbus;

    const float JUMP_POWER_AT_0_ENERGY = 0f;

    //Charge jump variables

    private float chargeStartTime;
    private bool isCharging;
    private Vector2 inputPosition;
    private bool shouldJump;
    private bool jumpDirection;
    private float jumpChargeDuration;
    private bool jumpFalling = false;


    void Start()
    {
        PauseMovement();
        viewManager = Camera.main.GetComponent<ViewManager>();
    }

    void Update()
    {
        if (GameManager.isGamePaused || nimbus.NimbusState == NimbusState.InTransition)
        {
            return;
        }
        // Handle touch input (for mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleInput(touch.phase, touch.position);
        }
        // Handle mouse input (for PC)
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            HandleInput(Input.GetMouseButtonDown(0) ? TouchPhase.Began : TouchPhase.Ended, Input.mousePosition);
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            // Check for boundary collision
            if (transform.position.x <= viewManager.LeftBoundary)
            {
                rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y);
            }
            else if (transform.position.x >= viewManager.RightBoundary)
            {
                rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y);
            }
        }

        if (!jumpFalling && rb.velocity.y < 0)
        {
            jumpFalling = true;
            NimbusEvents.TriggerOnJumpFalling();
        }

    }

    void HandleInput(TouchPhase phase, Vector2 position)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                chargeStartTime = Time.time;
                isCharging = true;
                inputPosition = position;
                break;

            case TouchPhase.Ended:
                if (isCharging)
                {
                    float chargeDuration = Mathf.Min(Time.time - chargeStartTime, maxChargeTime);
                    Vector3 screenPoint = mainCamera.ScreenToWorldPoint(inputPosition);
                    jumpDirection = screenPoint.x > 0;
                    jumpChargeDuration = chargeDuration;
                    Jump(jumpDirection, jumpChargeDuration);
                    jumpFalling = false;
                    isCharging = false;
                }
                break;
        }
    }

    void Jump(bool isJumpingRight, float chargeDuration)
    {
        // don't allow jump if Nimbus is falling or cloud power is 0
        if (cloudPower.CurrentCloudPower <= 0 || nimbus.NimbusState == NimbusState.Falling)
        {
            return;
        }

        rb.isKinematic = false;
        rb.velocity = Vector2.zero;

        if (chargeDuration < 0.2f)
        {
            // prevent taps that are too short to be counted as a charge
            chargeDuration = 0;
        }
        float t = chargeDuration / maxChargeTime;
        float verticalForce = Mathf.Lerp(minVerticalForce, maxVerticalForce, t);
        float horizontalForce = Mathf.Lerp(minHorizontalForce, maxHorizontalForce, t);

        if (isJumpingRight)
        {
            Flip(true);
        }
        else
        {
            horizontalForce = -horizontalForce;
            Flip(false);
        }

        // Check if there is enough cloud power to jump
        if (cloudPower.CurrentCloudPower <= 0)
        {
            verticalForce *= JUMP_POWER_AT_0_ENERGY;
            horizontalForce *= JUMP_POWER_AT_0_ENERGY;
            NimbusEvents.TriggerOnFalling();
        }
        else
        {
            NimbusEvents.TriggerOnJumped();
        }
        rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    void Flip(bool isJumpingRight)
    {
        bool isFacingRight = transform.localScale.x > 0;

        Vector3 scale = transform.localScale;
        if ((isFacingRight && !isJumpingRight) || (!isFacingRight && isJumpingRight))
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    void PauseMovement()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        isJumping = false;
    }
}