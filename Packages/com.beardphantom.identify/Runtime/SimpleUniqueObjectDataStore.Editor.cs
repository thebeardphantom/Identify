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
            try
            {
                var allData = AssetDatabase.FindAssets(
                        $"t:{nameof(ScriptableObject)}",
                        new[]
                        {
                            "Assets"
                        })
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                    .OfType<IUniqueObject>()
                    .Cast<ScriptableObject>()
                    .ToArray();
                AllData.Clear();
                AllData.AddRange(allData);
            }
            catch (UnityException)
            {
                // Need to aggressively look for assets but an exception is thrown if domain is being backed up during domain
                // reloading. No (?) way to detect domain reloads specifically so a try/catch must be used.
            }
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        #endregion
    }
}
#endif