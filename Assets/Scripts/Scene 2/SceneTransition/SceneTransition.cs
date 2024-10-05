using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private GameObject nimbusGO;
    private Nimbus nimbus;
    Rigidbody2D nimbusRb;
   private CloudPower script;

    void Start(){
      nimbusGO = GameObject.Find("Ninja Nimbus");
      nimbusRb = nimbusGO.GetComponent<Rigidbody2D>();
      nimbus = nimbusGO.GetComponent<Nimbus>();
      script = nimbusGO.GetComponent<CloudPower>();
    }
     void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Ninja Nimbus")
        {
            PauseNimbusMovement();
        }
     }

     public void BeginNimbusTransition(){
      Debug.Log("Nimbus Transition Starts");
         PauseNimbusMovement();
         nimbusRb.velocity = new Vector2(0, 10f);
         nimbus.NimbusState = NimbusState.InTransition;

         // change cloud depletion to zero
         script.StopDepleting();
     }

     public void EndNimbusTransition(){
         ResumeNimbusMovement();
         nimbus.NimbusState = NimbusState.Idle;

         // change cloud depletion back
         script.ContinueDepleting();
     }

     private void PauseNimbusMovement(){
        nimbusRb.isKinematic = true;
     }

     private void ResumeNimbusMovement(){
        nimbusRb.isKinematic = false;
     }
}
