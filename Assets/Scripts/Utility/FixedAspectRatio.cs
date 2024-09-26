using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectRatio : MonoBehaviour
{
    public float x = 16f;
    public float y = 9f;
    private float targetAspect => this.x / this.y;
    private Camera cam;

    private float scaleHeight;
    private float scaleWidth;


    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraRect();
    }

    /// <summary>
    /// Updates the camera's viewport rectangle to maintain a fixed aspect ratio.
    /// </summary>
    void UpdateCameraRect()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        scaleHeight = windowAspect / targetAspect;
        scaleWidth = 1.0f / scaleHeight;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }
    }

    /// <summary>
    /// Returns a Rect object representing the scaled viewport.
    /// </summary>
    public Rect GetScaledRect()
    {
        if (scaleHeight < 1.0f)
        {
            return new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            return new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }

}