using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbusFalling : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody2D rb;
    public Animator animator;

    void OnEnable()
    {
        NimbusEvents.OnFalling += StartFalling;
    }

    void OnDisable()
    {
        NimbusEvents.OnFalling -= StartFalling;
    }
    public void StartFalling()
    {
        rb.isKinematic = false;
        animator.Play("Fall");
    }
}
