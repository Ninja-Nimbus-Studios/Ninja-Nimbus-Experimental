using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    const int EASY_SPAWNCOUNT = 20; // number of spawns
    const int MID_SPAWNCOUNT = 40; // number of spawns
    private float timer = 0;
    public GameObject cloud; //reference to pipe
    private float prevHeight;
    private float newHeight;
    public GameObject nimbus;
    private int column;
    private const int MIN_COLUMN = 0;
    private const int MAX_COLUMN = 2;
    private float horizontalPos;
    private float prevPos;

    public static List<Vector3> cloudCoordinates = new List<Vector3>();
    public static List<Vector3> midCloudCoordinates = new List<Vector3>();
    public static int MAX_JUMP_COUNT = 0;

    // Constant Vairables 
    const float RIGHT_COLUMN = 3.1f;
    const float LEFT_COLUMN = -3.1f;
    const float CLOUD_DISTANCE = 3.1f; // CLOUD_DISTANCE should have same y as Nimbus vertical jump distance
    const float CLOUD_STARTING_HEIGHT = -0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeCloudSpawner();    
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitializeCloudSpawner()
    {
        // Clear previous coordinates from list
        cloudCoordinates.Clear();
        midCloudCoordinates.Clear();

        // Reset Variables
        prevHeight = newHeight = CLOUD_STARTING_HEIGHT; // starting height
        prevPos = 1f;

        // Spawn Clouds for different levels
        EasySpawnCloud(EASY_SPAWNCOUNT);
        MiddleSpawnCloud(MID_SPAWNCOUNT);

        // Log for debugging purposes
        // Debug.Log("*****");
        // for(int i = 0; i < midCloudCoordinates.Count; i++)
        // {
        //     Console.WriteLine(midCloudCoordinates[i]);
        // }

        MAX_JUMP_COUNT = cloudCoordinates.Count;
    }

    void EasySpawnCloud(int amount)
    {
        for (int i = 0; i < amount; i++)
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

            newCloud.transform.position = new Vector3(horizontalPos, newHeight, 0);
            cloudCoordinates.Add(newCloud.transform.position);
            prevHeight = newHeight;
            newHeight = prevHeight + CLOUD_DISTANCE;
            prevPos = horizontalPos;
        }
    }

    void MiddleSpawnCloud(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newCloud = Instantiate(cloud); // create new pipe

            // Random column decider
            column = UnityEngine.Random.Range(MIN_COLUMN, MAX_COLUMN);
            // Debug.Log(column);
            // Determine x coordinate for newly spawned cloud
            if(column == 0)
            {
                horizontalPos = LEFT_COLUMN;
            }
            else
            {
                horizontalPos = RIGHT_COLUMN;
            }

            Vector3 cloudPosition = new Vector3(horizontalPos, newHeight, 0);

            newCloud.transform.position = cloudPosition;
            // midCloudCoordinates.Add(cloudPosition); // midCloudCoordinates list is used for debugging
            cloudCoordinates.Add(cloudPosition);

            // Log for debugging purposes
            // Debug.Log(newCloud.transform.position);
            // Debug.Log("******");

            // Update variables
            prevHeight = newHeight;
            newHeight = prevHeight + CLOUD_DISTANCE;
        }
    }
}
 