using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float chunkHeight;
    public int cloudsPerChunk;
    public float minCloudDistance;
    public float maxCloudDistance;

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private float highestChunkY = 4f;
    private System.Random random;
    private GameObject nimbus;

    void Start()
    {
        random = new System.Random(); // Or use a seed for reproducible levels
        GenerateInitialChunks();
        nimbus = GameObject.Find("Ninja Nimbus");
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

    void GenerateChunk()
    {
        GameObject chunk = new GameObject($"Chunk_{highestChunkY}");
        Vector3 chunkPosition = new Vector3(0, highestChunkY, 0);
        chunk.transform.position = chunkPosition;

        for (int i = 0; i < cloudsPerChunk; i++)
        {
            Vector3 cloudPosition = GetNextCloudPosition(chunkPosition, i);
            GameObject cloud = Instantiate(cloudPrefab, cloudPosition, Quaternion.identity, chunk.transform);
            // Additional cloud setup if needed
        }

        activeChunks.Enqueue(chunk);
        highestChunkY += chunkHeight;
    }

    Vector3 GetNextCloudPosition(Vector3 chunkPosition, int cloudIndex)
    {
        float x = (float)(random.NextDouble() * 10 - 5); // Random X between -5 and 5
        float y = chunkPosition.y + (cloudIndex) * (chunkHeight / (cloudsPerChunk));
        return new Vector3(x, y, 0);
    }

    bool PlayerNeedsNewChunk()
    {
        // Implement logic to check if player is nearing the top of the highest chunk
        return nimbus.transform.position.y > highestChunkY - chunkHeight * 2;
    }

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
