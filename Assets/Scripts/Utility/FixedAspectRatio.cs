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

    private float scaleHeight;
    private float scaleWidth;

    // Reference to the UI container (usually the Canvas or a Panel)
    public RectTransform uiContainer;

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

        if (scaleHeight < 1.0f) // current screen is taller
        {
            // wider screen, pad black to bot and top
            // adjust UI padding by rectTransform.anchorMin.y == 0 vs 1
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            var centerHeight = (1.0f - scaleHeight) / 2.0f;
            rect.x = 0;
            rect.y = centerHeight;
            cam.rect = rect;

            Debug.Log($"Camsize: {cam.rect.size}, {cam.orthographicSize}");
            // Apply vertical padding to UI elements
            ApplyDynamicUIPadding(0, centerHeight * Screen.height);
        }
        else 
        {
            // wider screen, pad black to left and right
            // adjust UI padding by rectTransform.anchorMin.x == 0 vs 1
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;

            Debug.Log($"Camsize: {cam.rect.size}, {cam.orthographicSize}");
            // Apply horizontal padding to UI elements
            ApplyDynamicUIPadding((1.0f - scaleWidth) / 2.0f * Screen.width, 0);
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

            foreach (RectTransform ui in uiContainer.GetComponentsInChildren<RectTransform>())
            {
                if (ui.CompareTag("Paddable UI Component"))
                {
                    Debug.Log($"Working with {ui.name}");
                    // For screens that need vertical padding (i.e., screen is taller)
                    if (verticalPadding > 0)
                    {
                        if (ui.anchorMin.y < 0.5f)
                        {
                            Debug.Log($"{ui.name}: anchored at bottom!");
                            // Anchored to bottom, apply offset to bottom edge
                            ui.offsetMin = new Vector2(ui.offsetMin.x, ui.offsetMin.y + verticalPadding);
                        }
                        else if (ui.anchorMin.y >= 0.5f)
                        {
                            Debug.Log($"{ui.name}: anchored at top!");
                            // Anchored to top, apply offset to top edge
                            ui.offsetMax = new Vector2(ui.offsetMax.x, ui.offsetMax.y - verticalPadding);
                        }
                    }
                    // For screens that need horizontal padding (i.e., screen is wider)
                    else if (horizontalPadding > 0)
                    {
                        if (ui.anchorMin.x < 0.5f)
                        {
                            Debug.Log($"{ui.name}: anchored at left!");
                            // Anchored to left, apply offset to left edge
                            Debug.Log($"Before Adjustment:{ui.anchoredPosition}, {ui.anchorMin}, {ui.offsetMin}");
                            ui.offsetMin = new Vector2(ui.offsetMin.x + horizontalPadding, ui.offsetMin.y);
                            Debug.Log($"After Adjustment:{ui.anchoredPosition}, {ui.anchorMin}, {ui.offsetMin}");
                        }
                        else if (ui.anchorMin.x >= 0.5f)
                        {
                            Debug.Log($"{ui.name}: anchored at right!");
                            // Anchored to right, apply offset to right edge
                            Debug.Log($"Before Adjustment:{ui.anchoredPosition}, {ui.anchorMax}, {ui.offsetMax}");
                            ui.offsetMax = new Vector2(ui.offsetMax.x - horizontalPadding, ui.offsetMax.y);
                            Debug.Log($"After Adjustment:{ui.anchoredPosition}, {ui.anchorMax}, {ui.offsetMax}");
                        }
                    }
                }
            }
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