using UnityEngine;

namespace BeardPhantom.Identify
{
#if UNITY_2020
    public partial class UniqueScriptableObject : ScriptableObject,
        IUniqueScriptableObject,
        ISerializationCallbackReceiver
    {
        #region Fields

        [SerializeField]
        [HideInInspector]
        private string _hashString;

        #endregion

        #region Properties

        /// <inheritdoc />
        public Hash128 GuidHash { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            UpdateHashString();
#endif
        }

        /// <inheritdoc />
        public void OnAfterDeserialize()
        {
            GuidHash = Hash128.Parse(_hashString);
        }

        #endregion
    }

#else
    public partial class UniqueScriptableObject : ScriptableObject, IUniqueScriptableObject
    {
        #region Properties

        [field: SerializeField]
        [field: HideInInspector]
        public Hash128 GuidHash { get; private set; }

        #endregion
    }
#endif
}