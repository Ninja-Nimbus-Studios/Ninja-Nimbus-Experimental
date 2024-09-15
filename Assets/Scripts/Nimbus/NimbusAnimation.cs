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
    }

    void OnDisable(){
        NimbusEvents.OnJumped -= PlayJumpAnimation;
        NimbusEvents.OnCloudLatched -= PlayCloudLatchedAnimation;
        NimbusEvents.OnFalling -= PlayFallingAnimation;
    }

    void PlayJumpAnimation(){
        animator.Play("Jump");
    }

    void PlayCloudLatchedAnimation(){
        animator.Play("Latch");
    }

    void PlayFallingAnimation(){
        animator.Play("Fall");
    }
}
