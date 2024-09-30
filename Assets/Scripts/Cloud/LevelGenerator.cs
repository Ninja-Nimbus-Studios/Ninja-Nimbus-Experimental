using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    [Header("Cloud Generation")]
    public GameObject cloudPrefab;
    public float chunkHeight;
    public int cloudsPerChunk;
    public float minCloudDistance;
    public float maxCloudDistance;
    
    [SerializeField]private float rightBoundOffset;
    [SerializeField]private float leftBoundOffset;

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private float highestChunkY = 0f;
    private float newScreenTop;
    private const float CLOUDHEIGHT = 1f;
    private bool isFirstCloud = true;
    private System.Random random;
    private GameObject nimbus;
    private float screenTop;
    private float leftEdge;
    private Camera mainCamera;
    private ViewManager viewManager;
    

    void Start()
    {
        mainCamera = Camera.main;
        random = new System.Random(); // Or use a seed for reproducible levels
        GenerateInitialChunks();
        nimbus = GameObject.Find("Ninja Nimbus");
        screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y;
        newScreenTop = screenTop;
        viewManager = Camera.main.GetComponent<ViewManager>();
        // cloudHeight = cloudPrefab.GetComponent<MeshRenderer>().bounds.size.y; // MeshRenderer not working atm
    }

    void Update()
    {
        // Check player's position and generate/remove chunks as needed
        if (PlayerNeedsNewChunk())
        {
            GenerateChunk();
            RemoveDistantChunks();
        }
    }

    /// <summary>
    /// Creates a chunk which is a group of clouds defined by cloudsPerChunk and enqueues the chunk into activeChunk queue
    /// </summary>
    void GenerateChunk()
    {
        GameObject chunk = new GameObject($"Chunk_{highestChunkY}");
        Vector3 chunkPosition = new Vector3(0, highestChunkY, 0);
        Vector3 prevCloudPosition = new Vector3(0, highestChunkY, 0);
        Vector3 cloudPosition;
        chunk.transform.position = chunkPosition;

        for (int i = 0; i < cloudsPerChunk; i++)
        {
            if (isFirstCloud)
            {
                // For the first cloud of the first chunk, set position to (0, 0, 0)
                cloudPosition = Vector3.zero;
                isFirstCloud = false; // Disable the flag after the first cloud is generated
            }
            else
            {
                // For subsequent clouds, use the normal generation logic
                cloudPosition = GetNextCloudPosition(prevCloudPosition);
            }
            GameObject cloud = Instantiate(cloudPrefab, cloudPosition, Quaternion.identity, chunk.transform);
            prevCloudPosition = cloud.transform.position;
            // Debug.Log($"{prevCloudPosition}");
            // Additional cloud setup if needed
        }

        activeChunks.Enqueue(chunk);
        highestChunkY = prevCloudPosition.y;
    }

    /// <summary>
    /// Returns next cloud position's coordinate by using the previous cloud position.
    /// </summary>
    /// <returns>the following cloud's coordinate.</returns>
    Vector3 GetNextCloudPosition(Vector3 prevPosition)
    {
        // X coordinate is randomly generated in between left and right screen edge
        // Y coordinate is randomly generated in between previous cloud y position and offset equivalent to screen top
        float x = Random.Range(viewManager.LeftBoundary + leftBoundOffset, viewManager.RightBoundary - rightBoundOffset);
        float y = Random.Range(prevPosition.y + CLOUDHEIGHT, newScreenTop);
        float offset = y - prevPosition.y;
        newScreenTop += offset;

        return new Vector3(x, y, 0);
    }

    /// <summary>
    /// Checks whether the location that Nimbus is at is high enough to generate a new chunk or not
    /// </summary>
    /// <returns>
    /// Returns a boolean of true if new chunk should be generated, false otherwise.
    /// </returns>
    bool PlayerNeedsNewChunk()
    {
        // Implement logic to check if player is nearing the top of the highest chunk
        return nimbus.transform.position.y > highestChunkY - 4 * 2;
    }

    /// <summary>
    /// Destroys object reference for a chunk whenever Nimbus is far away from clouds that have been passed
    /// </summary>
    void RemoveDistantChunks()
    {
        if(activeChunks.Peek().transform.position.y < nimbus.transform.position.y - chunkHeight * 2)
        {
            Destroy(activeChunks.Dequeue());
        }
    }

    void GenerateInitialChunks()
    {
        // Generate a few chunks to start the game
    }
}
