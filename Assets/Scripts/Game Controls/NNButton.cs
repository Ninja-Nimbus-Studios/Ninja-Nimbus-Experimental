using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NNButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool isLeftButton;
    public NimbusJump nimbusAction;
    public void OnPointerDown(PointerEventData eventData){
        if(isLeftButton)
        {
            nimbusAction.OnLeftPressed();
        } 
        else
        {
            nimbusAction.OnRightPressed();
        }
    }

    public void OnPointerUp(PointerEventData eventData){
    }
}