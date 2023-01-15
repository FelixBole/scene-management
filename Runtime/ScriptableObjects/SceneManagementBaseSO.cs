using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Slax.SceneManagement
{
    ///<summary>Base for Scene Management SO classes. It is Serializable</summary>
    public class SceneManagementBaseSO : ScriptableObject
    {
        [TextArea] public string Description;

        [SerializeField, HideInInspector] private string _guid;
        public string Guid => _guid;

#if UNITY_EDITOR
    void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        _guid = AssetDatabase.AssetPathToGUID(path);
    }
#endif
    }
}