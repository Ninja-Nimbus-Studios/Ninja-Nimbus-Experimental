using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float spawnCount = 20; // the time interval for spawn
    private float timer = 0;
    public GameObject cloud; //reference to pipe
    private float prevHeight;
    private float newHeight;
    public GameObject nimbus;
    private int column;
    private float horizontalPos;
    private float prevPos;
    // Start is called before the first frame update
    void Start()
    {
        prevHeight = -3.1f;
        prevPos = -3.1f;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newCloud = Instantiate(cloud); // create new pipe
            // column = Random.Range(0,2);
            // if(column == 0)
            // {
            //     horizontalPos = 3f;
            // }
            // else
            // {
            //     horizontalPos = -3f;
            // }

            if(prevPos == 3.1f)
            {
                horizontalPos = -3.1f;
            }
            else 
            {
                horizontalPos = 3.1f;
            }
            newHeight = prevHeight + 3.1f;
            newCloud.transform.position = new Vector3(horizontalPos, newHeight, 0); // randomize height of pipe
            prevHeight = newHeight;
            prevPos = horizontalPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
 