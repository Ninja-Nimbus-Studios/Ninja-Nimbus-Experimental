using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CloudBarFollow : MonoBehaviour
{
    public Transform nimbus;  // Reference to Nimbus's Transform
    public Vector3 offset;    // Offset to keep the health bar above Nimbus

    void LateUpdate()
    {
        if (nimbus != null)
        {
            // Keep health bar at a fixed position relative to Nimbus
            transform.position = nimbus.position + offset;
        }
    }
}
