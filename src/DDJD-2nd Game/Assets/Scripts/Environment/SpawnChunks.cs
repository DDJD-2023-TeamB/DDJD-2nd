using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChunks : MonoBehaviour
{
    [SerializeField] private ChunkLoader[] _chunkLoaders;
    [SerializeField] private Transform _playerTransform;

    // Take into account the distance to the Chunk's Center (ChunkLoaders position is at the center)
    static float MAX_PLAYER_DISTANCE = (50 * Mathf.Sqrt(2)) + 70;

    Vector2 parseXZ(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate _chunkloaders
        for (int i = 0; i < this._chunkLoaders.Length; i++)
        {
            // Check if the player is close enough to the chunk loader
            if (Vector2.Distance(parseXZ(this._playerTransform.position), parseXZ(this._chunkLoaders[i].transform.position)) < MAX_PLAYER_DISTANCE)
            {
                this._chunkLoaders[i].LoadChunk();
            }
            else
            {
                this._chunkLoaders[i].UnloadChunk();
            }
        }
    }
}
