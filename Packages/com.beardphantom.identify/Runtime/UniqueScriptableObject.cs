using UnityEngine;

namespace BeardPhantom.Identify
{
    public partial class UniqueScriptableObject : ScriptableObject, IUniqueObject
    {
        #region Fields

        [SerializeField]
        [HideInInspector]
        private string _identifier;

        #endregion

        #region Properties

        /// <inheritdoc />

        public string Identifier
        {
            get
            {
#if UNITY_EDITOR
                if (string.IsNullOrWhiteSpace(_identifier))
                {
                    _identifier = RegenerateIdentifier();
                }
#endif
                return _identifier;
            }
        }

        #endregion
    }
}