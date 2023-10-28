using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify.Editor
{
    public static class IdentifyEditorActions
    {
        #region Methods

        [MenuItem("Assets/Identify/Reserialize All")]
        public static void ReserializeUniqueAssets()
        {
            var paths = SimpleUniqueObjectDataStore.GetUniqueAssetPaths().ToArray();
            AssetDatabase.ForceReserializeAssets(paths);
            AssetDatabase.SaveAssets();
            Debug.Log($"Reserialized the following assets:\n{string.Join(Environment.NewLine, paths)}");
        }

        #endregion
    }
}