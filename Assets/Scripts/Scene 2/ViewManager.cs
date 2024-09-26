using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Transform target;
    public Transform background;
    private Vector3 backgroundOffset;
    public float parallaxFactor; // Factor to control the speed of the parallax effect
    FixedAspectRatio fixedAspectRatio;
    public float LeftBoundary {get; private set;}
    public float RightBoundary {get; private set;}


    void Start()
    {
        backgroundOffset = background.position - transform.position;
        fixedAspectRatio = GetComponent<FixedAspectRatio>();
        ApplyLetterboxPadding();
        SetBoundary();
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

    public void ApplyLetterboxPadding()
    {
        fixedAspectRatio.UpdateCameraRect();
        
    }

    void SetBoundary(){
        
        LeftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        RightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
    }
}