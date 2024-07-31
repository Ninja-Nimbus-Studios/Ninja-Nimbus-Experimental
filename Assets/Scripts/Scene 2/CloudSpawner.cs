using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    const float RIGHT_COLUMN = 3.1f;
    const float LEFT_COLUMN = -3.1f;
    const float CLOUD_DISTANCE = 3.1f; // CLOUD_DISTANCE should have same y as Nimbus vertical jump distance
    const float CLOUD_STARTING_HEIGHT = -0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        prevHeight = newHeight = CLOUD_STARTING_HEIGHT; // starting height
        prevPos = 1f;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newCloud = Instantiate(cloud); // create new pipe

            // Determine x coordinate for newly spawned cloud
            if(prevPos == RIGHT_COLUMN)
            {
                horizontalPos = LEFT_COLUMN;
            }
            else
            {
                horizontalPos = RIGHT_COLUMN;
            }

            newCloud.transform.position = new Vector3(horizontalPos, newHeight, 0); // randomize height of pipe
            prevHeight = newHeight;
            newHeight = prevHeight + CLOUD_DISTANCE;
            prevPos = horizontalPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
 