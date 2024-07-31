using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NimbusJump : MonoBehaviour
{
    public Button left;
    public Button right;
    public float moveSpeed = 1f;
    public Rigidbody2D rb;

    private float moveX;
    private float moveY = 3.1f;
    private bool isJumping = false;
    private bool isStuck = true;

    // Different moving modality
    private Vector3 prevPosition;
    public Transform rightPoint;
    private Vector3 rightTarget = new Vector3(6.14f, 3.11f, 0f);
    public Transform leftPoint;
    private Vector3 leftTarget = new Vector3(-6.14f, 3.11f, 0f);
    private Vector3 targetPosition;
    private float MAX_DISTANCE = 4.39f;
    private bool isMoving = false;
    private bool jumpRight = false; 
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
        // Debug.Log($"{rightPoint.position}, {right}, {leftPoint.position}, {leftPoint}");
        // Debug.Log($"Character Position: {transform.position}");
        // Check if the object has landed and should stick
        // if (isStuck && rb.bodyType != RigidbodyType2D.Static)
        // {
        //     rb.velocity = Vector2.zero;
        //     rb.bodyType = RigidbodyType2D.Static;
        // }
        if(jumpRight)
        {
            targetPosition = prevPosition + rightTarget;
        }
        else
        {
            targetPosition = prevPosition + leftTarget;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Check if the position has reached the target
            if (Vector2.Distance(transform.position, targetPosition) <= 0.05f)
            {
                isMoving = false;
                // Debug.Log("Reached target position");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if(collision.gameObject.CompareTag("Cloud"))
        {
            Debug.Log("Collided with clouds..");
            isJumping = false;
            isStuck = true;
            rb.velocity = Vector2.zero;
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
        // moveX = 6.2f;
        // moveY = 3.1f;
        // rightPoint.position = transform.position + new Vector3(moveX, moveY);
        // Jump(moveX, moveY);
        SetCurrentPosition();
        isMoving = true;
        jumpRight = true;
    }

    private void JumpLeft()
    {
        // moveX = -6.2f;
        // moveY = 3.1f;
        // leftPoint.position = transform.position + new Vector3(moveX, moveY);
        // Jump(moveX, moveY);
        SetCurrentPosition();
        isMoving = true;
        jumpRight = false;
    }
    private void SetCurrentPosition()
    {
        prevPosition = transform.position;
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

    private void JumpTowards(Transform movePoint)
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        Debug.Log($"{transform.position}, {movePoint.position}, {moveSpeed * Time.deltaTime}");
    }
}
