using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] backgrounds; // Your 7 background prefabs
    public GameObject cloudLayer;    // Cloud layer to trigger transition
    private int currentBackgroundIndex = 0;

    void Start()
    {
        LoadNextBackground();
    }

    public void LoadNextBackground()
    {
        // Dynamically instantiate the next background 
        if (currentBackgroundIndex < backgrounds.Length)
        {
            Instantiate(backgrounds[currentBackgroundIndex], new Vector3(0, CalculateNextPosition(), 0), Quaternion.identity);
            currentBackgroundIndex++;
        }
    }

    private float CalculateNextPosition()
    {
        // Logic to calculate the position for the next background
        // You could use the current background's height and position to calculate this
        float backgroundHeight = 10f; // Example value; adjust based on actual background size
        return transform.position.y + backgroundHeight;
    }
}
