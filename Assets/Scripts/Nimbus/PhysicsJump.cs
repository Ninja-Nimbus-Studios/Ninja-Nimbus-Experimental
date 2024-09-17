using System;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce; // Set a positive number 

    //inspector header
    [Header("Component References")]
    public Rigidbody2D rb;
    private bool isJumping;
    private Camera mainCamera;
    public float leftBoundary;
    public float rightBoundary;
    [SerializeField] CloudPower cloudPower;
    [SerializeField] Nimbus nimbus;

    const float JUMP_POWER_AT_0_ENERGY = 0f;

    void Start()
    {
        PauseMovement();
        mainCamera = Camera.main;
        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (screenPoint.x > 0)
            {
                Jump(true);
            }
            else
            {
                Jump(false);
            }
        }

        if(isJumping)
        {
            // Check for boundary collision
            if (transform.position.x <= leftBoundary)
            {
                // Bounce off the left wall
                Debug.Log("Nimbus is at wall!");
                rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y); // Flip the x velocity
            }
            else if (transform.position.x >= rightBoundary)
            {
                // Bounce off the right wall
                Debug.Log("Nimbus is at wall!");
                rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y); // Flip the x velocity
            }
        }
    }

    void Jump(bool isJumpingRight)
    {
        // don't allow jump if Nimbus is falling or cloud power is 0
        if(cloudPower.CurrentCloudPower <= 0 || nimbus.NimbusState == NimbusState.Falling){
            return;
        }

        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        float jumpForce;

        // Differentiate between left and right jumps
        if(isJumpingRight){
            jumpForce = horizontalForce;
            Flip(true);
        }else{
            jumpForce = -horizontalForce;
            Flip(false);
        }

        // Check if there is enough cloud power to jump
        if(cloudPower.CurrentCloudPower <= 0){
            jumpForce *= JUMP_POWER_AT_0_ENERGY;
            NimbusEvents.TriggerOnFalling();
        }else{
            NimbusEvents.TriggerOnJumped();
        }

        rb.AddForce(new Vector2(jumpForce, verticalForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    //
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