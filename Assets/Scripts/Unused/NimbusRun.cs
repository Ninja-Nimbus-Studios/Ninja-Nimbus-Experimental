// UNUSED
// original game mechanic where nimbus is controlled by jumping up with physics perclick

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class NimbusRun : MonoBehaviour
{
    private Rigidbody2D rb;
    public float rightVelocity = 1;
    public float upVelocity = 1;
    private string gameStatus;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = Vector2.right * rightVelocity;
        // Debug.Log($"{PluginHelper.shouldJump}");
        if(Input.GetMouseButtonDown(0))
        {
            //Jump
            rb.velocity = Vector2.up * upVelocity;
        }
    }

    void OnBecameInvisible()
    {
        gameManager.GameOver();
    }
}
