// UNUSED
// Oriignal score mechanic for clouds using onTriggerEnter2D and collision detection

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudTouchOnRight : MonoBehaviour
{
    public GameObject point;
    [SerializeField] CapsuleCollider rCollider;
    [SerializeField] CapsuleCollider lCollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On right side");
        Score.score++;

        rCollider.enabled = false;
        lCollider.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        point.gameObject.SetActive(false); // disabling this will make the clouds disappear
    }
}
