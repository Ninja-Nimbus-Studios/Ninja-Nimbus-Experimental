using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    public GameObject coin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score.score++;
        coin.gameObject.SetActive(false);
        Debug.Log($"{coin.name}, {collision.gameObject.name}");
    }
}
