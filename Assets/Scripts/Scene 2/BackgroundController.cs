using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject[] backgrounds; // Your 7 background prefabs
    public GameObject cameraStopper;
    private int currentBackgroundIndex = 0;
    public GameObject backgroundGroup;

    void Start()
    {
        LoadNextBackground();
    }

    public void LoadNextBackground()
    {
        // Dynamically instantiate the next background 
        if (currentBackgroundIndex < backgrounds.Length)
        {
            Debug.Log("Next Background is rendering");
            Instantiate(backgrounds[currentBackgroundIndex], new Vector3(0, CalculateNextPosition(), 0), Quaternion.identity);
            currentBackgroundIndex++;
        } else {
            Debug.LogError("No more background available!");
        }
    }

    private float CalculateNextPosition()
    {
        // Logic to calculate the position for the next background
        // You could use the current background's height and position to calculate this
        float previousTop = cameraStopper.transform.position.y;
        float backgroundHeight = backgroundGroup.GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log($"{previousTop}, {backgroundHeight}");
        return previousTop + backgroundHeight;
    }
}
