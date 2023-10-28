using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class UniqueScriptableObject : ScriptableObject, IUniqueObject
    {
        #region Properties

        /// <inheritdoc />
        [field: SerializeField]
        [field: HideInInspector]
        public string IdentifierString { get; private set; }

        /// <inheritdoc />
        public PropertyName Identifier { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
#if UNITY_EDITOR
            SerializeInEditor();
#endif
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Identifier = new PropertyName(IdentifierString);
        }

        #endregion
    }
}