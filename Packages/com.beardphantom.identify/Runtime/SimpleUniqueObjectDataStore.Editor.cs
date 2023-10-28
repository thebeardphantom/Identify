#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class SimpleUniqueObjectDataStore
    {
        #region Methods

        private void Reset()
        {
            AllData.Clear();
            var allData = AssetDatabase.FindAssets(
                    "t:UniqueScriptableObject",
                    new[]
                    {
                        "Assets"
                    })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .Cast<UniqueScriptableObject>();
            AllData.AddRange(allData);
        }

        #endregion
    }
}
#endif