using UnityEngine;

namespace Slax.SceneManagement
{
    /// <summary>
    /// The location entrance is linked to a location exit of another scene
    /// The PathSO is what links the entrance to the exit
    /// </summary>
    public class LocationEntrance : MonoBehaviour
    {
        [SerializeField] private PathSO _entrancePath;
        public PathSO EntrancePath => _entrancePath;

        // [SerializeField] private PathStorageSO _pathStorage = default;
    }
}