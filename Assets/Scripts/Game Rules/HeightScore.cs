using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeightScore : MonoBehaviour
{
    [SerializeField]TMP_Text heightScoreField; 
    GameObject nimbus;
    float initialY;
    float yDiff => nimbus.transform.position.y - initialY;
    float highestScore;
    public float HighestScore => highestScore;

    void Start(){
        nimbus = GameObject.Find("Ninja Nimbus");
        initialY = nimbus.transform.position.y;
        heightScoreField.text = $"{Mathf.RoundToInt(0)} m";
    }

    void Update(){
        if(yDiff > highestScore){
            highestScore = yDiff;
            heightScoreField.text = $"{Mathf.RoundToInt(yDiff)} m";
        }
    }
}
