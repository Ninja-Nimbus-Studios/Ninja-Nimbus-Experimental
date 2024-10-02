using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    public GameObject cameraStopper;
    public GameObject backgroundPrefab; // Drag the background prefab into this field in the Inspector
    public Sprite[] backgroundSprites;  // Assign your background sprites here in the Inspector
    private float backgroundHeight = 11f;
    private int spriteIndex = 0;

    // Function to instantiate the background with a specific sprite int spriteIndex, Vector3 position
    // public void CreateNewBackground(Vector3 position)
    // {
    //     if (spriteIndex < 0 || spriteIndex >= backgroundSprites.Length)
    //     {
    //         Debug.LogError("Sprite index out of range.");
    //         return;
    //     }

    //     position += new Vector3(0, backgroundHeight, 0);

    //     // Instantiate the background prefab
    //     GameObject newBackground = Instantiate(backgroundPrefab, position, Quaternion.identity);

    //     // Assign the desired sprite
    //     SpriteRenderer spriteRenderer = newBackground.GetComponent<SpriteRenderer>();
    //     if (spriteRenderer != null)
    //     {
    //         Debug.Log($"Assigned new sprite: {backgroundSprites[spriteIndex]}");
    //         spriteRenderer.sprite = backgroundSprites[spriteIndex];
    //         spriteIndex++;
    //     }
    // }
}
