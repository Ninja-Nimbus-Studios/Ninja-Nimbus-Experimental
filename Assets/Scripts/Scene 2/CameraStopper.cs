using UnityEngine;

public class CameraStopper : MonoBehaviour
{
    public ViewManager cameraScript; // Reference to your camera follow script
    public BackgroundController backgroundManager; // Manager for changing the background

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.name == "Ninja Nimbus")
    //     {
    //         // switch reference from Main Camera script to the newly created 
    //     }
    // }

    // void OnBecameVisible()
    // {
    //     Debug.Log("Camera Stopper object is visible!");
    //     // Stop the camera from following Nimbus
    //     // cameraScript.StopFollowing();
    //     var backgroundHeight = transform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    //     var newPosition = transform.position;

    //     // Load the next background or perform any additional action
    //     backgroundManager.CreateNewBackground(transform.position);
    // }
}

