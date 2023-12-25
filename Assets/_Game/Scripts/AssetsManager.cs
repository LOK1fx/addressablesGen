using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Debug = UnityEngine.Debug;

public static class AssetsManager
{
    public static AsyncOperationHandle<T> Load<T>(string path)
    {
        return Addressables.LoadAssetAsync<T>(path);
    }

    public static AsyncOperationHandle<GameObject> InstantiateAsset(string path)
    {
        return InstantiateAsset(path, Vector3.zero, Quaternion.identity);
    }

    public static AsyncOperationHandle<GameObject> InstantiateAsset(string path, Vector3 position, Quaternion rotation)
    {
        var time = Stopwatch.StartNew();
        var operation = Addressables.InstantiateAsync(path, position, rotation);

        operation.Completed += (gameObject) =>
        {
            Debug.Log($"{gameObject.DebugName} loaded {time.Elapsed}");
            time.Stop();
        };

        return operation;
    }

    public static AsyncOperationHandle LoadWorldCell(string cell, Transform parent)
    {
        var time = Stopwatch.StartNew();
        var operation = Addressables.InstantiateAsync(cell, parent);

        operation.Completed += (gameObject) =>
        {
            Debug.Log($"{gameObject.DebugName} loaded: {time.Elapsed}");
            time.Stop();
        };

        return operation;
    }
}
