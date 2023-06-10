using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject _assetReference;

    private GameObject _chunkInstance;

    private bool _loading = false;  // This flag is used to prevent multiple async operations from being started at the same time

    /**
    * This method is called by the SpawnChunks script when the player enters the trigger.
    */
    public void LoadChunk() {
        if (this._assetReference == null || this._loading) return;

        if (!this._assetReference.IsValid()) {
            this._loading = true;
            
            // The asset reference is not loaded yet, so load it now
            this._assetReference.LoadAssetAsync<GameObject>()
                .Completed += AsyncOperationHandle_Completed;
        }
    }

    /**
    * This method is called by the SpawnChunks script when the player exits the trigger.
    */
    public void UnloadChunk() {
        if (this._assetReference == null || this._loading) return;

        if (this._assetReference.IsValid()) {
            this._loading = true;

            // Destroy the chunk instance
            GameObject.Destroy(this._chunkInstance);

            // Release the asset reference
            this._assetReference.ReleaseAsset();

            this._loading = false;
        }
    }

    /**
    * This method is called when the async operation is completed.
    */
    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            this._chunkInstance = Instantiate(asyncOperationHandle.Result);
        }
        else
        {
            Debug.Log("Failed to load asset");
        }
        this._loading = false;
    }
}
