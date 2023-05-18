using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject _assetReference;

    /**
    * This method is called by the SpawnChunks script when the player enters the trigger.
    */
    public void LoadChunk() {
        this._assetReference.LoadAssetAsync<GameObject>()
            .Completed += AsyncOperationHandle_Completed;
    }

    /**
    * This method is called by the SpawnChunks script when the player exits the trigger.
    */
    public void UnloadChunk() {
        this._assetReference.ReleaseAsset();    // TODO: Confirm this is the correct method to call
    }

    /**
    * This method is called when the async operation is completed.
    */
    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(asyncOperationHandle.Result);
        }
        else
        {
            Debug.Log("Failed to load asset");
        }
    }
}
