using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionExit : MonoBehaviour
{
    [SerializeField] SceneTransition sceneTransition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ninja Nimbus") sceneTransition.EndNimbusTransition();
    }
}
