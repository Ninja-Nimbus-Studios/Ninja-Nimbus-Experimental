using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public GameObject point;
    public GameObject cloud;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        point.gameObject.SetActive(false);
        cloud.gameObject.SetActive(false);
    }
}
