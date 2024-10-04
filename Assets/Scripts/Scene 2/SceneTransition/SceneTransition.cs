using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private GameObject nimbusGO;
    private Nimbus nimbus;
    Rigidbody2D nimbusRb;

    void Start(){
      nimbusGO = GameObject.Find("Ninja Nimbus");
      nimbusRb = nimbusGO.GetComponent<Rigidbody2D>();
      nimbus = nimbusGO.GetComponent<Nimbus>();
    }
     void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Ninja Nimbus")
        {
            PauseNimbusMovement();
        }
     }

     public void BeginNimbusTransition(){
      PauseNimbusMovement();
        nimbusRb.velocity = new Vector2(0, 10f);
        nimbus.NimbusState = NimbusState.InTransition;
     }

     public void EndNimbusTransition(){
        ResumeNimbusMovement();
         nimbus.NimbusState = NimbusState.Idle;
     }

     private void PauseNimbusMovement(){
        nimbusRb.isKinematic = true;
     }

     private void ResumeNimbusMovement(){
        nimbusRb.isKinematic = false;
     }
}
