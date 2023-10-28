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
            IdentifierString = GlobalObjectId.GetGlobalObjectIdSlow(this).ToString();
        }

        #endregion
    }
}

#endif