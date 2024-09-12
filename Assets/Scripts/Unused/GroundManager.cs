// UNUSED
// Logic to move the ground from right to left using repeated animation

using UnityEngine;
using UnityEngine.UI;

public class GroundManager : MonoBehaviour
{
    public Animator groundAnimator;
    const string MOVE_ANIM = "MoveGround";
    const string STOP_ANIM = "StopGround";
    public Button startButton;
    public Button stopButton;
    public Text buttonText;

    void Start()
    {
        if (groundAnimator == null)
        {
            groundAnimator = GetComponent<Animator>();
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(MoveGround);
        }
        else
        {
            Debug.LogWarning("Start Button is not assigned in the inspector.");
        }

        if (stopButton != null)
        {
            stopButton.onClick.AddListener(StopGround);
        }
        else
        {
            Debug.LogWarning("Start Button is not assigned in the inspector.");
        }

        // Ensure buttonText is assigned
        // if (buttonText == null)
        // {
        //     buttonText = startButton.GetComponentInChildren<Text>();
        // }
    }

    public void MoveGround()
    {
        Debug.Log("Start button clicked"); // Debug log to check if the function is called
        groundAnimator.SetTrigger(MOVE_ANIM);
        // Visual feedback: change button text color or text itself
        if (buttonText != null)
        {
            buttonText.color = Color.red; // Change color to red
            buttonText.text = "Animating..."; // Change text to "Clicked!"
        }
    }

    public void StopGround()
    {
        Debug.Log("Stop animation called"); // Debug log to check if the function is called
        groundAnimator.SetTrigger(STOP_ANIM);

        // Visual feedback: reset button text color or text itself
        if (buttonText != null)
        {
            buttonText.color = Color.black; // Change color to black
            buttonText.text = "Easy"; // Change text to "Start"
        }
    }
}