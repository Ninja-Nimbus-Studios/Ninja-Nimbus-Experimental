using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NimbusJump : MonoBehaviour
{
    public Button left;
    public Button right;
    public float moveSpeed = 20f;
    public Rigidbody2D rb;

    private float moveX;
    private float moveY = 15f;
    private bool isJumping = false;
    private bool isStuck = true;
    // Start is called before the first frame update
    void Start()
    {
        if(left != null)
        {
            left.onClick.AddListener(() => JumpRight());
        }

        if(right != null)
        {
            right.onClick.AddListener(() => JumpLeft());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object has landed and should stick
        // if (isStuck && rb.bodyType != RigidbodyType2D.Static)
        // {
        //     rb.velocity = Vector2.zero;
        //     rb.bodyType = RigidbodyType2D.Static;
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if(collision.gameObject.CompareTag("Cloud"))
        {
            Debug.Log("Collided!");
            isJumping = false;
            isStuck = true;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Cloud"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void JumpRight()
    {
        if (!isJumping && isStuck)
        {
            moveX = 5f;
            Jump(moveX, moveY);
        }
    }

    private void JumpLeft()
    {
        Debug.Log($"isJumping: {isJumping}, isStuck: {isStuck}");
        if (!isJumping && isStuck)
        {
            moveX = -5f;
            Jump(moveX, moveY);
        }
    }
    private void Jump(float x, float y)
    {
        if (rb != null)
        {
            // Apply the force to make the object jump left up diagonally
            rb.velocity = Vector2.zero; // Reset current velocity
            rb.AddForce(new Vector2(moveX, moveY), ForceMode2D.Impulse);
            isJumping = true;
            isStuck = false;
        }
    }
}
