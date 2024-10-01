using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour
{
    public CharacterMonitor cameraFollowScript; // Reference to your camera follow script
    public BackgroundController backgroundManager; // Manager for changing the background

    void OnBecameVisible()
    {
        Debug.Log("Camera Stopper object is visible!");
        // Stop the camera from following Nimbus
        cameraFollowScript.StopFollowing();

        // Load the next background or perform any additional action
        backgroundManager.LoadNextBackground();
    }
}

