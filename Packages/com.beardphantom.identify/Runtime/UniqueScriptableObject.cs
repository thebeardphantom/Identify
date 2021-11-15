using UnityEngine;

namespace BeardPhantom.Identify
{
#if UNITY_2020
    public partial class UniqueScriptableObject : ScriptableObject, IUniqueScriptableObject
    {
        #region Fields

        [SerializeField]
        private string _hashString;

        #endregion

        #region Properties

        /// <inheritdoc />
        public Hash128 GuidHash { get; private set; }

        #endregion
    }

#else
    public partial class UniqueScriptableObject : ScriptableObject, IUniqueScriptableObject
    {
        #region Properties

        [field: SerializeField]
        public Hash128 GuidHash { get; private set; }

        #endregion
    }
#endif
}