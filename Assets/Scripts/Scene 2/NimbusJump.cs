using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Vector3 upTarget = new Vector3(0f, 3.11f, 0f);
    private Vector3 targetPosition;
    private float MAX_DISTANCE = 4.39f;
    private bool isMoving = false;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool jumpUp = false; 
    // Start is called before the first frame update
    void Start()
    {
        if(left != null)
        {
            left.onClick.AddListener(() => OnLeftPressed());
        }

        if(right != null)
        {
            right.onClick.AddListener(() => OnRightPressed());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Check if the position has reached the target
            if (Vector2.Distance(transform.position, targetPosition) <= 0.05f)
            {
                isMoving = false;
                jumpUp = false;
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
    void OnLeftPressed()
    {
        leftPressed = true;
        Debug.Log("Invoking from Left");
        Invoke("BothButtonPress", 0.1f);
        Debug.Log("Finished Invoking from left");
    }

    void OnRightPressed()
    {
        rightPressed = true;
        Debug.Log("Invoking from Right");
        Invoke("BothButtonPress", 0.1f);
        Debug.Log("Finished Invoking from Right");
    }

    private void BothButtonPress()
    {
        if(rightPressed && leftPressed)
        {
            JumpUp();
        }
        else if(!rightPressed && !jumpUp)
        {
            JumpLeft();
        }
        else if(!leftPressed && !jumpUp)
        {
            JumpRight();
        }

        // Reset button states after checking
        leftPressed = false;
        rightPressed = false;
    }
    private void JumpRight()
    {
        Debug.Log("Right Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + rightTarget;
        isMoving = true;
    }

    private void JumpLeft()
    {
        Debug.Log("Left Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + leftTarget;
        isMoving = true;
    }

    private void JumpUp()
    {
        Debug.Log("Up Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + upTarget;
        isMoving = true;
        jumpUp = true;
    }
    private void SetCurrentPosition()
    {
        prevPosition = transform.position;
    }
}
