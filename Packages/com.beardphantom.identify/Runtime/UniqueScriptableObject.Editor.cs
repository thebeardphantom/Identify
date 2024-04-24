#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class UniqueScriptableObject : ISerializationCallbackReceiver
    {
        #region Fields

        public const string IDENTIFIER_PROPERTY_NAME = nameof(_identifier);

        #endregion

        #region Methods

        internal string RegenerateIdentifier()
        {
            var globalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(this);
            var identifier = AssetDatabase.IsMainAsset(this)
                ? globalObjectId.assetGUID.ToString()
                : globalObjectId.ToString()["GlobalObjectId_V1-".Length..];
            return identifier;
        }

        [ContextMenu("Print Identifier")]
        private void PrintIdentifier()
        {
            Debug.Log(Identifier, this);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _identifier = RegenerateIdentifier();
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        #endregion
    }
}

#endif