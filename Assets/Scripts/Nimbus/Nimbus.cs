using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimbus : MonoBehaviour
{
    
    public NimbusState NimbusState;
    public void SwitchStateToJumping() => SwitchStateTo(NimbusState.Jumping);
    public void SwitchStateToLatching() => SwitchStateTo(NimbusState.Latching);
    public void SwitchStateToFalling() => SwitchStateTo(NimbusState.Falling);
    public void SwitchStateTo(NimbusState newState){
        NimbusState = newState;
    }

    void Start(){
        NimbusState = NimbusState.Idle;
    }
    void OnEnable(){
        NimbusEvents.OnJumped += SwitchStateToJumping;
        NimbusEvents.OnCloudLatched += SwitchStateToLatching;
        NimbusEvents.OnFalling += SwitchStateToFalling;
    }
    void OnDisable(){
        NimbusEvents.OnJumped -= SwitchStateToJumping;
        NimbusEvents.OnCloudLatched -= SwitchStateToLatching;
        NimbusEvents.OnFalling -= SwitchStateToFalling;
    }
}

public enum NimbusState{
    Idle,
    Jumping,
    Latching,
    Falling
}
