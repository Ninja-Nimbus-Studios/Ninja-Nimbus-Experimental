using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxTime = 1; // the time interval for spawn
    private float timer = 0;
    public GameObject pipe; //reference to pipe
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime) {
            GameObject newPipe = Instantiate(pipe); // create new pipe
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0); // randomize height of pipe
            Destroy(newPipe, 15); // destroy pipe after 15 seconds so that you aren't creating too many
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
 