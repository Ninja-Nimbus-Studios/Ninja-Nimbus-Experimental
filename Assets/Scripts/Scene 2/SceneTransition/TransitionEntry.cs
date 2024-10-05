using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionEntry : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ninja Nimbus")
        {
            sceneTransition.BeginNimbusTransition();
            Debug.Log($"Transition Entry: {gameObject.name}");
        }
    }
}
