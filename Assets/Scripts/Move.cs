using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static float speed;
    private bool updated;
    private int prevScore;
    // Start is called before the first frame update
    void Start()
    {
        updated = true;
        prevScore = Score.score;
    }

    // Update is called once per frame
    void Update()
    {
        if(Score.score - prevScore > 0)
        {
            updated = false;
        }
        if(!updated)
        {
            GameManager.speedOfPipe += (float)0.2;
            updated = true;
        }
        transform.position += Vector3.left * GameManager.speedOfPipe * Time.deltaTime;
        prevScore = Score.score;
        Debug.Log($"{GameManager.speedOfPipe}");
    }
}
