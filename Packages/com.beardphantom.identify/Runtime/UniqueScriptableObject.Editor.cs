#if UNITY_EDITOR
using UnityEngine;

namespace BeardPhantom.Identify
{
#if UNITY_2020
    public partial class UniqueScriptableObject : ISerializationCallbackReceiver
    {
        #region Methods

        /// <inheritdoc />
        public void OnBeforeSerialize()
        {
            _hashString = IdentifyUtility.GetObjectIdAsHash128(this).ToString();
        }

        /// <inheritdoc />
        public void OnAfterDeserialize()
        {
            GuidHash = Hash128.Parse(_hashString);
        }

        #endregion
    }

#else
    public partial class UniqueScriptableObject : ISerializationCallbackReceiver
    {
        #region Methods

        /// <inheritdoc />
        public void OnBeforeSerialize()
        {
            GuidHash = IdentifyUtility.GetObjectIdAsHash128(this);
        }

        /// <inheritdoc />
        public void OnAfterDeserialize() { }

        #endregion
    }
#endif
}
#endif