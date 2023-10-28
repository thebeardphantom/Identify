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
                    "t:ScriptableObject",
                    new[]
                    {
                        "Assets"
                    })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .OfType<IUniqueObject>()
                .Cast<ScriptableObject>();
            AllData.AddRange(allData);
            AllData.Remove(this);
        }

        #endregion
    }
}
#endif