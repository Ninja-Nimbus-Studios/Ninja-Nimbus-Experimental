// UNUSED
// For creating haptics on iOS devices

using UnityEngine;
// using CandyCoded.HapticFeedback;
using UnityEngine.UI;
// using Interhaptics.Core;

public class HapticManager : MonoBehaviour{

    [SerializeField] private Button lowButton;
    [SerializeField] private Button medButton;
    [SerializeField] private Button highButton;
    [SerializeField] private Button interButton;

    private void OnEnable() {
        lowButton.onClick.AddListener(LightVibration);
        medButton.onClick.AddListener(MediumVibration);
        highButton.onClick.AddListener(HeavyVibration);
        interButton.onClick.AddListener(InterDefaultVibration);
    }

    private void OnDisable() {
        lowButton.onClick.RemoveListener(LightVibration);
        medButton.onClick.RemoveListener(MediumVibration);
        highButton.onClick.RemoveListener(HeavyVibration);
        interButton.onClick.RemoveListener(InterDefaultVibration);
    }

    public void DefaultVibration(){
        Debug.Log("Default Vibration Triggered!");
        Handheld.Vibrate();
    }

    public void InterDefaultVibration(){
        Debug.Log("InterHaptics Vibration Triggered!");
        // HAR.PlayConstant(1.0, 2);
    }

    public void LightVibration(){
        Debug.Log("Light Vibration Triggered!");
        // HapticFeedback.LightFeedback();
    }

    public void MediumVibration(){
        Debug.Log("Medium Vibration Triggered!");
        // HapticFeedback.MediumFeedback();
    }

    public void HeavyVibration(){
        Debug.Log("Heavy Vibration Triggered!");
        // HapticFeedback.HeavyFeedback();
    }
}