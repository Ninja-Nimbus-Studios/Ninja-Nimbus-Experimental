using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NimbusJump : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isJumping = false;
    private bool isStuck = true;

    // Motion and Coordinates
    public GameManager gameManager;
    private Vector3 prevPosition;
    private Vector3 rightTarget = new Vector3(6.14f, 3.11f, 0f);
    private Vector3 leftTarget = new Vector3(-6.14f, 3.11f, 0f);
    private Vector3 upTarget = new Vector3(0f, 3.11f, 0f);
    private Vector3 latchRightOffset = new Vector3(-1.1f, -0.42f, 0f);
    private Vector3 latchLeftOffset = new Vector3(1.3f, -0.35f, 0f);
    private Vector3 offset;
    private Vector3 targetPosition;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool bothButtonsPressed = false;
    public static int jumpCount; // this variable keep scount of how many times it jumped which is the same as cloud index
    public const string DIRECTION_U = "up";
    public const string DIRECTION_L = "left";
    public const string DIRECTION_R = "right";

    // Interaction with Clouds
    private SpriteRenderer cloudIndicator;
    private Animator nimbusAnimator;
    private string prevPos;
    private bool isMidAirAnimSet;

    void Start()
    {
        moveSpeed = 55f;
        jumpCount = 0;

        if (nimbusAnimator == null)
        {
            nimbusAnimator = GetComponent<Animator>();
        }

        SetPreviousPosition();
    }

    void Update()
    {
        if (PlayerPrefs.GetString("Status") == GameManager.STATUS_JUMP)
        {
            // Check previous state and play animation
            if(!isMidAirAnimSet)
            {
                SetMidAirAnimation();
                targetPosition += offset;
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the position has reached the target
            if (transform.position == targetPosition)
            {
                LatchAnimation();
                cloudIndicator = CloudSpawner.clouds[jumpCount].transform.GetChild(3).GetComponent<SpriteRenderer>();
                cloudIndicator.color = new Color(0,255,0);
                PlayerPrefs.SetString("Status", GameManager.STATUS_REST);

                jumpCount++;
                bothButtonsPressed = false;
                SetPreviousPosition();
                isMidAirAnimSet = false;
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
    public void OnLeftPressed()
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

    public void OnRightPressed()
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
            PlayerPrefs.SetString("Direction", DIRECTION_U);
            PlayerPrefs.SetString("AirDirection", DIRECTION_U);
            JumpTowardsTarget();

        }
        else if(!rightPressed && IsDirectionCorrect(DIRECTION_L))
        {
            Debug.Log("Jump Left!");
            PlayerPrefs.SetString("Direction", DIRECTION_L);
            PlayerPrefs.SetString("AirDirection", DIRECTION_L);
            JumpTowardsTarget();
        }
        else if(!leftPressed && IsDirectionCorrect(DIRECTION_R))
        {
            Debug.Log("Jump Right!");
            PlayerPrefs.SetString("Direction", DIRECTION_R);
            PlayerPrefs.SetString("AirDirection", DIRECTION_R);
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
        Checks which direction nimbus should end on
    */
    private void LatchAnimation()
    {
        string endDirection = PlayerPrefs.GetString("Direction");
        if(endDirection == DIRECTION_R)
        {
            nimbusAnimator.SetTrigger("Right");
            // Debug.Log("Sets latch RIGHT");
        }
        else
        {
            nimbusAnimator.SetTrigger("Left");
            // Debug.Log("Sets latch LEFT");
        }
    }

    /*
        Sets animation during mid air action. If Nimbus jumps right from left, or up from right,
        then the animation trigger should be AirRight and if it jumps left from right or up from left, 
        then the animation trigger should be AirLeft. This function also adjusts the offset you need for
        Nimbus to visually latch onto cloud.
    */
    private void SetMidAirAnimation()
    {
        isMidAirAnimSet = true;
        string airDirection = PlayerPrefs.GetString("AirDirection");
        if(prevPos == DIRECTION_R)
        {
            if(airDirection == DIRECTION_L)
            {
                nimbusAnimator.SetTrigger("AirLeft");
                offset = latchLeftOffset;
                Debug.Log("Sets Air LEFT -> 1.");
            }
            else if(airDirection == DIRECTION_R)
            {
                nimbusAnimator.SetTrigger("AirRight");
                offset = latchRightOffset;
                Debug.Log("Sets Air RIGHT -> 2.");
            }
            else
            {
                nimbusAnimator.SetTrigger("AirRight");
                offset = latchRightOffset;
                Debug.Log("Sets Air RIGHT -> 3.");
            }
        }
        else
        {
            if(airDirection == DIRECTION_R)
            {
                nimbusAnimator.SetTrigger("AirRight");
                offset = latchRightOffset;
                Debug.Log("Sets Air RIGHT -> 4.");
            }
            else if(airDirection == DIRECTION_L)
            {
                nimbusAnimator.SetTrigger("AirLeft");
                offset = latchLeftOffset;
                Debug.Log("Sets Air LEFT -> 5.");
            }
            else 
            {
                nimbusAnimator.SetTrigger("AirLeft");
                offset = latchLeftOffset;
                Debug.Log("Sets Air LEFT -> 6.");
            }
        }
    }

    /*
        Sets prevPos variable to the previous position. It can be either right or left
    */
    private void SetPreviousPosition()
    {
        if (transform.position.x > CloudSpawner.clouds[jumpCount].transform.position.x)
        {
            prevPos = DIRECTION_R;
            Debug.Log("Previous Position: RIGHT");
        }
        else if (transform.position.x < CloudSpawner.clouds[jumpCount].transform.position.x)
        {
            prevPos = DIRECTION_L;
            Debug.Log("Previous Position: LEFT");
        }
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
