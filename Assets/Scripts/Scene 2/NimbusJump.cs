using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NimbusJump : MonoBehaviour
{
    public Button left;
    public Button right;
    public float moveSpeed = 50f;
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
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool bothButtonsPressed = false;
    private bool jumpUp = false;
    
    // dying mechanic
    public GameManager gameManager;
    public static int scoreBeforeJump;
    public static int scoreAfterJump;
    public static int jumpCount = 0; // this variable keep scount of how many times it jumped which is the same as cloud index
    public const string DIRECTION_U = "up";
    public const string DIRECTION_L = "left";
    public const string DIRECTION_R = "right";

    // UI changes from jump
    // public CloudMechanic cloudMechanic;

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
        scoreBeforeJump = scoreAfterJump = Score.score;
        moveSpeed = 55f;
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Status") == GameManager.STATUS_JUMP)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Debug.Log($"{moveSpeed}, {targetPosition}");
            // Check if the position has reached the target
            if (transform.position == CloudSpawner.cloudCoordinates[jumpCount])
            {
                // jumpUp = false;
                // cloudMechanic.ChangeCloud();
                PlayerPrefs.SetString("Status", GameManager.STATUS_REST);
                jumpCount++;
                bothButtonsPressed = false;
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
        Debug.Log("Left Pressed");
        // Check if this jump click was done mid jump
        if(!IsDoubleJump())
        {
            leftPressed = true;
            Invoke("BothButtonPress", 0.1f);
        }
        else
        {
            Debug.Log("You lost: Double Jump not allowed!");
            gameManager.GameOver();
        }
    }

    void OnRightPressed()
    {
        Debug.Log("Right Pressed");
        // Check if this jump click was done mid jump
        if(!IsDoubleJump())
        {
            rightPressed = true;
            Invoke("BothButtonPress", 0.1f);
        }
        else
        {
            Debug.Log("You lost: Double Jump not allowed!");
            gameManager.GameOver();
        }
    }

    /*
        BothButtonPress checks for if the both of the jump buttons were clicked at the same time or not.
        Jump function is not called and GameOver is called if user picks the wrong direction to jump.
    */    
    private void BothButtonPress()
    {
        if (bothButtonsPressed){
            return; // Prevent multiple invocations
        }
        bothButtonsPressed = true;

        if(rightPressed && leftPressed && IsDirectionCorrect(DIRECTION_U))
        {
            Debug.Log("Jump Up!");
            JumpTowardsTarget();
        }
        else if(!rightPressed && IsDirectionCorrect(DIRECTION_L))
        {
            Debug.Log("Jump Left!");
            JumpTowardsTarget();
        }
        else if(!leftPressed && IsDirectionCorrect(DIRECTION_R))
        {
            Debug.Log("Jump Right!");
            JumpTowardsTarget();
        }
        else
        {
            if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMECLEAR)
            {
                Debug.Log("You won!");
                gameManager.GameCleared();

            }
            else
            {
                Debug.Log("You lost: wrong direction to jump!");
                gameManager.GameOver();
            }
        }

        // Reset button states after checking
        leftPressed = false;
        rightPressed = false;
    }

    /*
        Triggers Right Jump
    */
    private void JumpRight()
    {
        Debug.Log("Right Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + rightTarget;

        // Set Game Status
        PlayerPrefs.SetString("Status", GameManager.STATUS_JUMP);
    }

    /*
        Triggers Left Jump
    */
    private void JumpLeft()
    {
        Debug.Log("Left Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + leftTarget;

        // Set Game Status
        PlayerPrefs.SetString("Status", GameManager.STATUS_JUMP);
    }

    /*
        Triggers Up Jump
    */
    private void JumpUp()
    {
        Debug.Log("Up Clicked!");
        SetCurrentPosition();
        targetPosition = prevPosition + upTarget;

        // Set Game Status
        PlayerPrefs.SetString("Status", GameManager.STATUS_JUMP);
        jumpUp = true;
    }

    /*
        Triggers jump action towards the correct target
    */
    private void JumpTowardsTarget()
    {
        // Debug.Log("Nimbus is jumping towards correct direction!");
        targetPosition = CloudSpawner.cloudCoordinates[jumpCount];

        // Set Game Status
        PlayerPrefs.SetString("Status", GameManager.STATUS_JUMP);
    }

    /*
        Checks if the jump direction is the correct direction or not.
        Input: direction to check
        Return: boolean
    */
    private bool IsDirectionCorrect(string direction)
    {
        if(CloudSpawner.MAX_JUMP_COUNT > jumpCount)
        {
            var nextCoordinate = CloudSpawner.cloudCoordinates[jumpCount];
            if(direction == DIRECTION_U)
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}");
                return nextCoordinate.x == transform.position.x;
            }
            else if(direction == DIRECTION_L)
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}");
                return nextCoordinate.x < transform.position.x;
            }
            else
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}");
                return nextCoordinate.x > transform.position.x;
            }
        }
        else {
            Debug.Log("jumpCount at max!");
            PlayerPrefs.SetString("Status", GameManager.STATUS_GAMECLEAR);
            return false;
        }
    }

    /*
        Checks if jump is called while Nimbus is jumping
        Return: boolean
    */
    private bool IsDoubleJump()
    {
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_JUMP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SetCurrentPosition()
    {
        prevPosition = transform.position;
    }
}
