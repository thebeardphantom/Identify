#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class UniqueScriptableObject : ISerializationCallbackReceiver
    {
        #region Methods

        private void SerializeInEditor()
        {
            var globalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(this);
            Identifier = AssetDatabase.IsMainAsset(this)
                ? globalObjectId.assetGUID.ToString()
                : globalObjectId.ToString()["GlobalObjectId_V1-".Length..];
        }

        #endregion
    }
}

#endif