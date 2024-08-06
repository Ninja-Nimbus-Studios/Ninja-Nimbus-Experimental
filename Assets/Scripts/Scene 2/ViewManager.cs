using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Transform target;
    public Transform background;
    private Vector3 backgroundOffset;
    public float parallaxFactor = 0.01f; // Factor to control the speed of the parallax effect


    void Start()
    {
        backgroundOffset = background.position - transform.position;
        Debug.Log($"BackgroundOffset: {backgroundOffset.ToString()}");
        parallaxFactor = 0.015f; // Factor to control the speed of the parallax effect
    }

    private void LateUpdate()
    {
        if(target.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = newPosition;

            Vector3 backgroundNewPosition = new Vector3(transform.position.x, target.position.y - target.position.y * parallaxFactor, transform.position.z);
            background.position = backgroundNewPosition + backgroundOffset;
        }
    }
}