using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudMechanic : MonoBehaviour
{
    public GameObject point;
    public GameObject cloud;
    public GameObject alternateCloudPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        point.gameObject.SetActive(false);
        cloud.gameObject.SetActive(false);
    }

    public void ChangeCloud()
    {
        // Instantiate the alternate cloud at the current cloud's position and rotation
        Instantiate(alternateCloudPrefab, transform.position, transform.rotation);
        
        // Destroy the current cloud
        Destroy(gameObject);
    }
}
