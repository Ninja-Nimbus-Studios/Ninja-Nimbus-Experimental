using UnityEngine;
using UnityEngine.UI;

//Not using this script because it doesn't work ;)
public class CanvasScalerController : MonoBehaviour
{
    public FixedAspectRatio fixedAspectRatio;
    private CanvasScaler canvasScaler;
    [SerializeField] RectTransform safeArea;
    private Camera mainCamera;

    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        mainCamera = Camera.main;
        
        if (fixedAspectRatio == null)
        {
            fixedAspectRatio = mainCamera.GetComponent<FixedAspectRatio>();
        }

        if (fixedAspectRatio != null && safeArea != null)
        {
            AdjustCanvas();
        }
        else
        {
            Debug.LogError("FixedAspectRatio component or SafeArea not found!");
        }
    }

    void Update()
    {
        // Continuously update the canvas to handle resolution changes
        AdjustCanvas();
    }

    void AdjustCanvas()
    {
        Rect scaledRect = fixedAspectRatio.GetScaledRect();

        // Set the canvas scaler to scale with screen size
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 1920); // Your original resolution
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = scaledRect.height < 1.0f ? 1 : 0; // Match height if letterboxing, width if pillarboxing

        // Convert the camera's viewport rect to canvas space
        Vector2 viewportMin = mainCamera.ViewportToScreenPoint(new Vector3(scaledRect.xMin, scaledRect.yMin, 0));
        Vector2 viewportMax = mainCamera.ViewportToScreenPoint(new Vector3(scaledRect.xMax, scaledRect.yMax, 0));

        // Convert screen space coordinates to canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvasScaler.transform,
            viewportMin,
            null,
            out Vector2 canvasMin
        );
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvasScaler.transform,
            viewportMax,
            null,
            out Vector2 canvasMax
        );

        // Adjust the safe area rect transform to match the game view in canvas space
        safeArea.anchorMin = Vector2.zero;
        safeArea.anchorMax = Vector2.one;
        safeArea.offsetMin = canvasMin;
        safeArea.offsetMax = canvasMax;
    }
}