// UNUSED
// Attempt to refactor jump class because jump class becoming too big

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NimbusJumpRefactor : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 newPosition;

    // Motion and Coordinates
    private Vector3 offset;
    private static Vector3 targetPosition;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool bothButtonsPressed = false;
    public static int jumpCount; // this variable keep scount of how many times it jumped which is the same as cloud index

    // Interaction with Clouds
    private SpriteRenderer cloudIndicator;
    private Animator nimbusAnimator;
    private string prevPos;
    private bool isMidAirAnimSet;

    // Constants
    private Vector3 latchRightOffset = new Vector3(-1.1f, -0.42f, 0f);
    private Vector3 latchLeftOffset = new Vector3(1.3f, -0.35f, 0f);
    public const string DIRECTION_U = "up";
    public const string DIRECTION_L = "left";
    public const string DIRECTION_R = "right";
    private const int PASSING_FINAL_SCORE = 5;

    void Start()
    {
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
            try
            {
                // Check previous state and play animation
                if(!isMidAirAnimSet)
                {
                    SetMidAirAnimation();
                    Debug.Log($"Update:\noffset:{offset}, targetPosition:{targetPosition}");
                    targetPosition += offset;
                }

                Debug.Log($"Nimbus starting at:{transform.position}");
                newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                // newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                Debug.Log($"Moving from {transform.position} to {newPosition}");
                transform.position = newPosition;
                Debug.Log($"New position: {transform.position}");

                // Check if the position has reached the target
                if (transform.position == targetPosition)
                {
                    LatchAnimation();
                    Debug.Log($"{CloudSpawner.clouds[jumpCount]}");
                    cloudIndicator = CloudSpawner.clouds[jumpCount].transform.GetChild(3).GetComponent<SpriteRenderer>();
                    cloudIndicator.color = new Color(0,255,0);
                    PlayerPrefs.SetString("Status", GameManager.STATUS_REST);
                    Debug.Log("NimbusJump Update: Status set to REST");
                    jumpCount++;

                    bothButtonsPressed = false;
                    isMidAirAnimSet = false;
                    SetPreviousPosition();

                    // If Nimbus is at the end, check game clear or over
                    Debug.Log($"NimbusJump Update: {jumpCount}/{CloudSpawner.MAX_JUMP_COUNT}");
                    if(jumpCount == CloudSpawner.MAX_JUMP_COUNT)
                    {
                        if(IsGameStageClearSuccessful()) // check if game clear successful or not
                        {
                            PlayerPrefs.SetString("Status", GameManager.STATUS_GAMECLEAR);
                            Debug.Log("You won!");
                        }
                        else
                        {
                            PlayerPrefs.SetString("Status", GameManager.STATUS_GAMEOVER);
                            Debug.Log("You Lost!");
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.LogException(ex, this);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if(collision.gameObject.CompareTag("Cloud"))
        {
            Debug.Log("Collided with clouds..");
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
            // Invoke("BothButtonPress", 0.1f);
        }
        else
        {
            Debug.Log("You lost: Double Jump not allowed!");
            Mistake.mistake++;
        }
    }

    public void OnRightPressed()
    {
        Debug.Log("Right Pressed");
        // Check if this jump click was done mid jump
        if(!IsDoubleJump())
        {
            rightPressed = true;
            // Invoke("BothButtonPress", 0.1f);
        }
        else
        {
            Debug.Log("You lost: Double Jump not allowed!");
            Mistake.mistake++;
        }
    }

    /*
        BothButtonPress checks for if the both of the jump buttons were clicked at the same time or not.
        Jump function is not called and GameOver is called if user picks the wrong direction to jump.
    */    
    private void BothButtonPress()
    {
        if (bothButtonsPressed){
            Debug.Log("Both Button Pressed already!");
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
        else // If Jump count is max, or if direction is wrong then it goes through here
        {
            if(CloudSpawner.MAX_JUMP_COUNT >= jumpCount) // if jump count is less than max, then player not at end yet
            {
                Debug.Log("Point deducted: Wrong direction to jump!");
                Mistake.mistake++;
                // Make a time delay for penalty and add more time in the beginning
            }
        }

        // Reset button states after checking
        leftPressed = false;
        rightPressed = false;
        bothButtonsPressed = false;
    }

    /*
        Triggers jump action towards the correct target
    */
    private void JumpTowardsTarget()
    {
        targetPosition = CloudSpawner.cloudCoordinates[jumpCount];
        Debug.Log($"JumpTowardsTarget:{targetPosition}");

        // Set Game Status
        PlayerPrefs.SetString("Status", GameManager.STATUS_JUMP);
        Debug.Log("NimbusJump.cs JumpTowardsTarget: JUMP");
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
        Sets prevPos variable to the previous position. It can be either right or left.
        This is used to set mid air animation.
    */
    private void SetPreviousPosition()
    {
        if (jumpCount == CloudSpawner.MAX_JUMP_COUNT)
        {
            return;
        }

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

    public void NextCloudPosition()
    {
        
    }

    /*
        Checks if the jump direction is the correct direction or not.
        Input: direction to check
        Return: boolean
    */
    private bool IsDirectionCorrect(string direction)
    {
        if(CloudSpawner.MAX_JUMP_COUNT >= jumpCount)
        {
            var nextCoordinate = CloudSpawner.cloudCoordinates[jumpCount];
            if(direction == DIRECTION_U)
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}, {Mathf.Abs(nextCoordinate.x - transform.position.x) < 1.31}");
                return Mathf.Abs(nextCoordinate.x - transform.position.x) < 1.31;
            }
            else if(direction == DIRECTION_L)
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}, {nextCoordinate.x < transform.position.x}");
                return nextCoordinate.x < transform.position.x;
            }
            else
            {
                Debug.Log($"Cloud:{nextCoordinate.x}, {transform.position.x}, {nextCoordinate.x > transform.position.x}");
                return nextCoordinate.x > transform.position.x;
            }
        }
        else {
            Debug.Log("jumpCount at max!");
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

    private bool IsGameStageClearSuccessful()
    {
        if(Score.finalScore < PASSING_FINAL_SCORE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
