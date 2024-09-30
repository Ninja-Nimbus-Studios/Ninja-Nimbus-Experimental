using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusAnimation : MonoBehaviour
{
    [Header("Component References")]
    public Animator animator;

    void OnEnable(){
        NimbusEvents.OnJumped += PlayJumpAnimation;
        NimbusEvents.OnCloudLatched += PlayCloudLatchedAnimation;
        NimbusEvents.OnFalling += PlayFallingAnimation;
        NimbusEvents.OnJumped += ResetFallState;
        NimbusEvents.OnJumpFalling += BeginFallState;
    }

    void OnDisable(){
        NimbusEvents.OnJumped -= PlayJumpAnimation;
        NimbusEvents.OnCloudLatched -= PlayCloudLatchedAnimation;
        NimbusEvents.OnFalling -= PlayFallingAnimation;
        NimbusEvents.OnJumped -= ResetFallState;
        NimbusEvents.OnJumpFalling -= BeginFallState;
    }

    void PlayJumpAnimation(){
        animator.Play("Jump_Up");
    }

    void PlayCloudLatchedAnimation(){
        animator.Play("Latch");
    }

    void PlayFallingAnimation(){
        animator.Play("Fall");
    }

    void BeginFallState(){
        animator.SetBool("IsJumpFalling", true);
    }
    void ResetFallState(){
        animator.SetBool("IsJumpFalling", false);
    }
}
