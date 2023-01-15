using UnityEngine;

namespace Slax.SceneManagement
{
    /// <summary>
    /// This class goes on a trigger which, when entered, sends the player to another Location
    /// </summary>
    public class LocationExit : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _locationToLoad;
        [SerializeField] private PathSO _leadsToPath;
        [SerializeField] private PathStorageSO _pathStorage;

        [Header("Broadcasting on")]
        [SerializeField] private LoadEventChannelSO _locationExitLoadChannel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_pathStorage != null)
                    _pathStorage.LastPathTaken = _leadsToPath;

                _locationExitLoadChannel.RaiseEvent(_locationToLoad, false, true);
            }
        }
    }
}