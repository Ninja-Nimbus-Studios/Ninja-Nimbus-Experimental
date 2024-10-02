using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectRatio : MonoBehaviour
{
    public float x = 16f;
    public float y = 9f;
    private float targetAspect => this.x / this.y;
    private Camera cam;

    public float ScaleHeight{get; private set;}
    public float ScaleWidth {get; private set;}

    // Reference to the UI container (usually the Canvas or a Panel)
    public RectTransform uiContainer;
    public RectTransform canvasRect;
    public bool ApplyPadding {get; private set;}
    [SerializeField] CanvasScalerController[] canvasScalerControllers; //we don't have that many so let's manually assign them

    /// <summary>
    /// Updates the camera's viewport rectangle to maintain a fixed aspect ratio.
    /// </summary>
    public void UpdateCameraRect()
    {
        cam = GetComponent<Camera>();
        float windowAspect = (float)Screen.width / (float)Screen.height;
        ScaleHeight = windowAspect / targetAspect;
        ScaleWidth = 1.0f / ScaleHeight;

        if (ScaleHeight < 1.0f) // current screen is taller
        {
            // wider screen, pad black to bot and top
            // adjust UI padding by rectTransform.anchorMin.y == 0 vs 1
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = ScaleHeight;
            var centerHeight = (1.0f - ScaleHeight) / 2.0f;
            rect.x = 0;
            rect.y = centerHeight;
            cam.rect = rect;
            
            ApplyPaddingToCanvasScalerControllers();
        }
        else 
        {
            // wider screen, pad black to left and right
            // adjust UI padding by rectTransform.anchorMin.x == 0 vs 1
            float ScaleWidth = 1.0f / ScaleHeight;
            Rect rect = cam.rect;
            rect.width = ScaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - ScaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;

            //do not apply padding
        }
    }

    void ApplyPaddingToCanvasScalerControllers()
    {
        foreach (CanvasScalerController controller in canvasScalerControllers)
        {
            Debug.Log($"Controller: {controller}");
            controller.AdjustCanvas(ScaleHeight);
        }
    }

    /// <summary>
    /// Dynamically applies padding to UI elements so they stay within the game frame.
    /// </summary>
    /// <param name="horizontalPadding">Amount of padding for the left and right edges.</param>
    /// <param name="verticalPadding">Amount of padding for the top and bottom edges.</param>
    void ApplyDynamicUIPadding(float horizontalPadding, float verticalPadding)
    {
        if (uiContainer != null)
        {
            // Apply padding to the RectTransform of the UI container
            // OffsetMin adjusts the lower-left corner (x, y)
            // OffsetMax adjusts the upper-right corner (x, y)
            Debug.Log($"Screensize: {Screen.width}, {Screen.height}");
            Debug.Log($"Padding: {horizontalPadding}, {verticalPadding}");
            Debug.Log($"{Screen.width - horizontalPadding}, {Screen.height - verticalPadding}");

            if(canvasRect)
            uiContainer.sizeDelta = canvasRect.sizeDelta;
            uiContainer.anchorMin = new Vector2(0.5f, 0.5f + (1 - ScaleHeight) / 2);
            uiContainer.anchorMax = new Vector2(0.5f, 0.5f - (1 - ScaleHeight) / 2);
        }
        else 
        {
            Debug.LogError($"uiContainer has no reference! Have you assigned it through Unity GUI?");
        }
    }

    /// <summary>
    /// Returns a Rect object representing the scaled viewport.
    /// This function is not being used right now. 
    /// </summary>
    public float GetScaleHeight()
    {
        return ScaleHeight;
    }

}