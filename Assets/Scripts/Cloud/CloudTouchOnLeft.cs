// UNUSED
// Oriignal score mechanic for clouds using onTriggerEnter2D and collision detection

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudTouchOnLeft : MonoBehaviour
{
    public GameObject cloud;
    [SerializeField] CapsuleCollider2D rCollider;
    [SerializeField] CapsuleCollider2D lCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Left side");
        Debug.Log($"Trigger Entered by {collision.gameObject.name}");
        if (collision.gameObject.name == "Ninja Nimbus")
        {
            Score.score++;

            rCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // cloud.gameObject.SetActive(false); // disabling this will make the clouds disappear
    }
}
