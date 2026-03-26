using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform player;

    public int chunksOnScreen = 5;
    public float chunkLength = 30f;

    private float spawnZ = 0f;
    private List<GameObject> activeChunks = new List<GameObject>();

    void Start()
    {
        spawnZ = player.position.z;

        for (int i = 0; i < chunksOnScreen; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (chunksOnScreen * chunkLength))
        {
            SpawnChunk();
        }

        if (player.position.z > activeChunks[0].transform.position.z + chunkLength)
        {
            DeleteChunk();
        }
    }

    void SpawnChunk()
    {
        GameObject chunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]);
        chunk.transform.SetParent(transform);
        chunk.transform.position = new Vector3(0, 0, spawnZ);

        spawnZ += chunkLength;
        activeChunks.Add(chunk);
    }

    void DeleteChunk()
    {
        Destroy(activeChunks[0]);
        activeChunks.RemoveAt(0);
    }
}