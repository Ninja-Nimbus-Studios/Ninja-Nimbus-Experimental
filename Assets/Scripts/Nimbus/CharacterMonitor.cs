// Logic to trigger death when Nimbus reaches the left or right border of screen

using UnityEditor;
using UnityEngine;

public class CharacterMonitor : MonoBehaviour
{
    public Transform character; // Reference to the character transform
    public float leftBoundary; // Adjust this value based on your screen's left boundary
    public float rightBoundary;
    public float bottomBoundary; // Bottom boundary of the screen
    public GameManager gameManager; // Reference to the GameManager
    private Camera mainCamera;
    private bool gameContinues;

    void Start()
    {
        mainCamera = Camera.main;
        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x;
        gameContinues = true;
    }

    void Update()
    {
        if (gameContinues)
        {
            bottomBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            // Check if the character's position is beyond the left boundary
            // if (character.position.x < leftBoundary || character.position.x > rightBoundary)
            if(character.position.y < bottomBoundary)
            {
                // Call the GameOver function in the GameManager
                gameManager.GameOver();
                gameContinues = false;
            }
        }
    }
}