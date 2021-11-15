#if UNITY_EDITOR
#if !UNITY_2020
using UnityEngine;
#endif
namespace BeardPhantom.Identify
{
#if UNITY_2020
    public partial class UniqueScriptableObject
    {
        #region Methods

        private void UpdateHashString()
        {
            _hashString = IdentifyUtility.GetObjectIdAsHash128(this).ToString();
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