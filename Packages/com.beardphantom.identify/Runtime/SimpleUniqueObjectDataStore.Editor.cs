#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class SimpleUniqueObjectDataStore : ISerializationCallbackReceiver
    {
        #region Methods

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            AllData.Clear();
            var allData = AssetDatabase.FindAssets(
                    $"t:{nameof(ScriptableObject)}",
                    new[]
                    {
                        "Assets"
                    })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .OfType<IUniqueObject>()
                .Cast<ScriptableObject>();
            AllData.AddRange(allData);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        #endregion
    }
}
#endif