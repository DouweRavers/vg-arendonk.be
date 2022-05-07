using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AddressableLoader : MonoBehaviour
{
    [SerializeField]
    AssetReference scene;
    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive).Completed += (obj) => 
        {
            if (obj.Status == AsyncOperationStatus.Succeeded) {
                Debug.Log("Loaded");
            }
        };
    }
}
