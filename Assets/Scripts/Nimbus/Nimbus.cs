using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Nimbus : MonoBehaviour
{
    
    public NimbusState NimbusState;
    public void SwitchStateToJumping() => SwitchStateTo(NimbusState.Jumping);
    public void SwitchStateToLatching() => SwitchStateTo(NimbusState.Latching);
    public void SwitchStateToFalling() => SwitchStateTo(NimbusState.Falling);
    public void SwitchStateToIdle() => SwitchStateTo(NimbusState.Idle);
    public void SwitchStateToTransition() => SwitchStateTo(NimbusState.InTransition);
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
        SubscribeSoundsToEvent();
    }
    void OnDisable(){
        NimbusEvents.OnJumped -= SwitchStateToJumping;
        NimbusEvents.OnCloudLatched -= SwitchStateToLatching;
        NimbusEvents.OnFalling -= SwitchStateToFalling;
        UnsubscribeSoundsFromEvent();
    }

    void SubscribeSoundsToEvent(){
        NimbusEvents.OnJumped += PlayJumpSound;
        NimbusEvents.OnCloudLatched += PlayLatchSound;
        NimbusEvents.OnFalling += PlayFallSound;
    }

    void UnsubscribeSoundsFromEvent(){
        NimbusEvents.OnJumped -= PlayJumpSound;
        NimbusEvents.OnCloudLatched -= PlayLatchSound;
        NimbusEvents.OnFalling -= PlayFallSound;
    }
    void PlayJumpSound(){
        AudioManager.Instance.PlaySound("Jump");
    }
    void PlayLatchSound(){
        AudioManager.Instance.PlaySound("Latch");
    }
    void PlayFallSound(){
        AudioManager.Instance.PlaySound("Fall");
    }




}

public enum NimbusState{
    Idle,
    Jumping,
    Latching,
    Falling,
    InTransition
}
