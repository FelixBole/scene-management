using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Slax.EventSystem;

namespace Slax.SceneManagement
{
    /// <summary>
    /// Allows a "cold start" in the editor, when pressing Play and not passing from the Initialisation scene.
    /// </summary> 
    public class EditorColdStartup : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private GameSceneSO _thisSceneSO = default;
        [SerializeField] private GameSceneSO _persistentManagersSO = default;
        [SerializeField] private AssetReference _notifyColdStartupChannel = default;
        [SerializeField] private VoidEventChannelSO _onSceneReadyChannel = default;
        [SerializeField] private PathStorageSO _pathStorage = default;

        [Tooltip("Create a void event for the save system to listen to.")]
        [SerializeField] private VoidEventChannelSO _onRequestSaveFileCreation = default;

        private bool isColdStart = false;
        private void Awake()
        {
            if (!SceneManager.GetSceneByName(_persistentManagersSO.SceneReference.editorAsset.name).isLoaded)
            {
                isColdStart = true;

                //Reset the path taken, so the character will spawn in this location's default spawn point
                _pathStorage.LastPathTaken = null;
            }

            CreateSaveIfNoFile();
        }

        void LogComplete(AsyncOperationHandle<SceneInstance> si)
        {
            Debug.Log($"Done {si.Result.Scene.name}");
        }

        private void Start()
        {
            if (isColdStart)
            {
                _persistentManagersSO.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
            }
        }

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        {
            _notifyColdStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoaded;
        }

        private void OnNotifyChannelLoaded(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            Debug.Log("Ready. The GameManager should fire on scene ready now.");
            // StartCoroutine(WaitBeforeRaisingEvent(obj));
            // if (_thisSceneSO != null)
            // {
            //     obj.Result.RaiseEvent(_thisSceneSO);
            // }
            // else
            // {
            //     // Raise a fake scene ready event, so the player is spawned
            //     _onSceneReadyChannel.RaiseEvent();
            //     // When this happens, the player won't be able to move between scenes because the SceneLoader has no conception of which scene we are in
            // }
        }

        IEnumerator WaitBeforeRaisingEvent(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            yield return new WaitForSeconds(2);

            if (_thisSceneSO != null)
            {
                obj.Result.RaiseEvent(_thisSceneSO);
            }
            else
            {

                // Raise a fake scene ready event, so the player is spawned
                _onSceneReadyChannel.RaiseEvent();
                // When this happens, the player won't be able to move between scenes because the SceneLoader has no conception of which scene we are in
            }

        }

        /// <summary>Should be captured by a save system to create save data if non is existent</summary>
        private void CreateSaveIfNoFile()
        {
            if (_onRequestSaveFileCreation != null)
                _onRequestSaveFileCreation.RaiseEvent();
        }

        // Unload and reload the current scene for persistent managers to be ready for events fired by player spawned in
        private void UnloadThisScene(AsyncOperationHandle<SceneInstance> sceneInstance)
        {
            // Unload ThisSceneSO which was active
            _thisSceneSO.SceneReference.UnLoadScene().Completed += (op) =>
            {
                Debug.Log($"Unloaded {op}");
            };
        }

        private void ReloadThisScene(AsyncOperationHandle<SceneInstance> obj)
        {
            Debug.Log($"Reloading");
            _thisSceneSO.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
        }
#endif
    }
}