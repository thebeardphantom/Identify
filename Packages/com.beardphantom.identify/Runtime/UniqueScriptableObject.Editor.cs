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
            Identifier = GlobalObjectId.GetGlobalObjectIdSlow(this).ToString();
        }

        #endregion
    }
}

#endif