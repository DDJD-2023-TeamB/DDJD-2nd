using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChunks : MonoBehaviour
{
    [SerializeField] private ChunkLoader[] _chunkLoaders;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (this._chunkLoaders.Length > 0)
            {
                this._chunkLoaders[0].LoadChunk();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (this._chunkLoaders.Length > 0)
            {
                this._chunkLoaders[0].UnloadChunk();
            }
        }
    }
}
