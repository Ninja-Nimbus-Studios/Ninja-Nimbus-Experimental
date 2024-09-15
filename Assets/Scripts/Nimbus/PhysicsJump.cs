using System;
using UnityEngine;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce; // Set a positive number 

    //inspector header
    [Header("Component References")]
    public Rigidbody2D rb;
    [SerializeField] CloudPower cloudPower;

    void Start()
    {
        PauseMovement();
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
    }

    void Jump(bool isJumpingRight)
    {
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
            jumpForce *= 0.25f;
            NimbusEvents.TriggerOnFalling();
        }else{
            NimbusEvents.TriggerOnJumped();
        }
        rb.AddForce(new Vector2(jumpForce, verticalForce), ForceMode2D.Impulse);
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
    }
}