using UnityEditor;
using UnityEngine;

public class CharacterMonitor : MonoBehaviour
{
    public Transform character; // Reference to the character transform
    public float leftBoundary; // Adjust this value based on your screen's left boundary
    public float rightBoundary; 
    public GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        Camera mainCamera = Camera.main;
        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x;
    }

    void Update()
    {
        // Check if the character's position is beyond the left boundary
        if (character.position.x < leftBoundary || character.position.x > rightBoundary)
        {
            // Call the GameOver function in the GameManager
            gameManager.GameOver();
        }
    }
}
