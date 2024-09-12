// UNUSED
// Oriignal score mechanic for clouds using onTriggerEnter2D and collision detection

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudMechanic : MonoBehaviour
{
    public GameObject point;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        point.gameObject.SetActive(false); // disabling this will make the clouds disappear
    }
}
