using UnityEngine;
using UnityEngine.UI;

// Not using this script because it doesn't work ;)
// Oh NOOOOOOO.....
public class CanvasScalerController : MonoBehaviour
{
    [SerializeField] RectTransform safeArea;
    [SerializeField] RectTransform canvasRect;
    public void AdjustCanvas(float scaleHeight)
    {
        if (safeArea == null || canvasRect == null)
        {
            Debug.Log($"safeArea: {safeArea}, canvasRect: {canvasRect}");
            Initialize();
        }
        safeArea.sizeDelta = canvasRect.sizeDelta;
        safeArea.anchorMin = new Vector2(0.5f, 0.5f + (1 - scaleHeight) / 2);
        safeArea.anchorMax = new Vector2(0.5f, 0.5f - (1 - scaleHeight) / 2);
    }

    private void Initialize(){
        safeArea = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }
}