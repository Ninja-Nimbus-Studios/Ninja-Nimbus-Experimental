using System.Data;
using UnityEngine;

public class NimbusLatch : MonoBehaviour
{
    public Rigidbody2D rb;
    public float latchSpeed = 5f;
    private bool isLatching = false;
    private Vector3 latchTarget;
    private Collider2D currentCloudTrigger;
    private Vector3 latchLeftOffset = new Vector2(-1.19f, -0.37f);
    private Vector3 latchRightOffset = new Vector2(1.3f, -0.35f);
    //(-4.29, 2.53) - (-3.1, 2.9)
    
    void Update()
    {
        if (isLatching)
        {
            // Move Nimbus towards the latch point
            transform.position = Vector2.MoveTowards(transform.position, latchTarget, latchSpeed * Time.deltaTime);

            // Check if Nimbus has reached the latch point
            if (Vector2.Distance(transform.position, latchTarget) < 0.1f)
            {
                Debug.Log($"Velocity: {rb.velocity}");
                CompleteLatch();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // Check if the other collider is one of the left or right points
        if (other.gameObject.name == "Point Right" || other.gameObject.name == "Point Left")
        {
            // Determine which trigger (right or left point) was hit
            if (other.gameObject.name == "Point Right")
            {
                // Latch to the right side of the cloud
                latchTarget = other.transform.parent.position + latchRightOffset;
                Debug.Log($"Right: {latchTarget}");
            }
            else if (other.gameObject.name == "Point Left")
            {
                // Latch to the left side of the cloud
                latchTarget = other.transform.parent.position + latchLeftOffset;
                Debug.Log($"Left: {latchTarget}");
            }

            // Trigger latch behavior
            StartLatchingMotion(other);
        }
    }


    private void StartLatchingMotion(Collider2D cloudTrigger)
    {
        // Disable physics and prepare to move manually
        rb.isKinematic = true;
        isLatching = true;
        rb.velocity = Vector2.zero;
        currentCloudTrigger = cloudTrigger;

        // Optionally: Play latch animation here
        // Disable cloud trigger after latching to prevent further interactions

        // cloudTrigger.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void CompleteLatch()
    {
        // Stop latching, Nimbus is now "latched" onto the cloud
        isLatching = false;
        Debug.Log("Got to position");

        // Optionally: Play the "latched" animation here
    }

    public void ReleaseLatch()
    {
        // Called when the user taps to jump off the cloud
        // rb.isKinematic = false;  // Re-enable physics
        isLatching = false;

        // Enable cloud trigger again if needed
        // if (currentCloudTrigger != null)
        // {
        //     currentCloudTrigger.enabled = true;
        // }
    }
}
