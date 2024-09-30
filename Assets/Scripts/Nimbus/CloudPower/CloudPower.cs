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
    [field: SerializeField] public float CloudJumpDepletion { get; set; }
    private CloudPowerDisplay cloudPowerDisplay;
    [SerializeField] bool TestWithNoDepletion = false;

    private void Start()
    {
        CurrentCloudPower = MaxCloudPower;
        cloudPowerDisplay = FindObjectOfType<CloudPowerDisplay>();
        if(cloudPowerDisplay == null)
        {
            Debug.LogError("CloudPowerDisplay not found in scene!");
        }

        if(TestWithNoDepletion)
        {
            CloudJumpDepletion = 0;
            CloudPowerDepletionRate = 0;
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
        CurrentCloudPower -= CloudJumpDepletion;
        if(CurrentCloudPower > 0 && CurrentCloudPower < 10){
            CurrentCloudPower = 0;
        }
    }
}
