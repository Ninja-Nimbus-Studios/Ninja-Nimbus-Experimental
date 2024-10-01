using UnityEngine;

public class NextLayerTrigger : MonoBehaviour
{
    public BackgroundController backgroundController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Cloud Layer")
        {
            // Trigger big leap and next background loading
            backgroundController.LoadNextBackground();
            // PerformBigLeap(other.gameObject);
        }
    }

    void PerformBigLeap(GameObject nimbus)
    {
        // Apply a strong vertical force or animate Nimbus into the next stage
        Rigidbody2D rb = nimbus.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, 1000f)); // Example force, adjust as needed
    }
}
