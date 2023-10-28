#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class UniqueScriptableObject : ISerializationCallbackReceiver
    {
        #region Methods

        [ContextMenu("Print Identifier")]
        private void PrintIdentifier()
        {
            Debug.Log(Identifier, this);
        }

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