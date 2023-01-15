using UnityEngine;

namespace Slax.SceneManagement
{
    /// <summary>
    /// ONLY CREATE ONE
    /// Stores, during gameplay, the path that was last taken
    /// </summary>
    [CreateAssetMenu(fileName = "PathStorage", menuName = "Scene Management/Path Storage")]
    public class PathStorageSO : SceneManagementBaseSO
    {
        [HideInInspector] public PathSO LastPathTaken;
    }
}