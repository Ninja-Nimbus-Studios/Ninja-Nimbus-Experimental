using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudPower : MonoBehaviour
{
    [field: SerializeField] public float MaxCloudPower { get; set; }
    public float CurrentCloudPower { get; set; }
    [field: SerializeField] public float CloudPowerDepletionRate { get; set; }
    [field: SerializeField] public float CloudRechargeRate { get; set; }
    private CloudPowerDisplay cloudPowerDisplay;

    private void Start()
    {
        CurrentCloudPower = MaxCloudPower;
        cloudPowerDisplay = FindObjectOfType<CloudPowerDisplay>();
        if(cloudPowerDisplay == null)
        {
            Debug.LogError("CloudPowerDisplay not found in scene!");
        }
    }

    private void OnEnable(){
        NimbusEvents.OnCloudLatched += RechargeCloudPower;
        NimbusEvents.OnJumped += JumpCloudPower;
    }

    private void OnDisable(){
        NimbusEvents.OnCloudLatched -= RechargeCloudPower;
        NimbusEvents.OnJumped -= JumpCloudPower;
    }

    private void Update(){
        cloudPowerDisplay.UpdateCloudPowerDisplay(CurrentCloudPower, MaxCloudPower);

        if(CurrentCloudPower > 0){
            CurrentCloudPower -= CloudPowerDepletionRate * Time.deltaTime;
        }
    }

    public void RechargeCloudPower(){
        CurrentCloudPower += CloudRechargeRate;
        if(CurrentCloudPower > MaxCloudPower){
            CurrentCloudPower = MaxCloudPower;
        }
    }

    public void JumpCloudPower(){
        CurrentCloudPower -= 20;
        if(CurrentCloudPower > 0 && CurrentCloudPower < 10){
            CurrentCloudPower = 0;
        }
    }
}
