using System;
using UnityEngine;

public class PhysicsJump : MonoBehaviour 
{
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce; // Set a positive number 
    public Rigidbody2D rb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (screenPoint.x > 0)
            {
                JumpRight();
            }
            else
            {
                JumpLeft();
            }
        }
    }

    void JumpRight()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
    }

    void JumpLeft()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(-horizontalForce, verticalForce), ForceMode2D.Impulse);
    }
}