using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor; // Factor to control the speed of the parallax effect
    private Vector3 previousCameraPosition;

    void Start()
    {
        // // Initialize the previous camera position
        // if (Camera.main != null)
        // {
        //     previousCameraPosition = Camera.main.transform.position;
        // }
    }

    void Update()
    {
        // // Move the background layer based on the camera's movement
        // if (Camera.main != null)
        // {
        //     Vector3 deltaMovement = Camera.main.transform.position - previousCameraPosition;
        //     transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);
        //     previousCameraPosition = Camera.main.transform.position;
        // }
        // Debug.Log($"{Camera.main.transform.position}");
        // transform.position.y = Camera.main.transform.position.y;
    }
}
