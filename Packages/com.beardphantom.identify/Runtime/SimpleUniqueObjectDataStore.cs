using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(SimpleUniqueObjectDataStore))]
    public partial class SimpleUniqueObjectDataStore : ScriptableObject, IUniqueObjectDataStore, ISerializationCallbackReceiver
    {
        #region Fields

        private readonly Dictionary<string, IUniqueObject> _idToObject = new();

        #endregion

        #region Properties

        public IEnumerable<ScriptableObject> Data => AllData;

        [field: SerializeField]
        private List<ScriptableObject> AllData { get; set; } = new();

        #endregion

        #region Methods

        /// <inheritdoc />
        public bool TryFindUniqueObject(string identifier, out IUniqueObject result)
        {
            return _idToObject.TryGetValue(identifier, out result);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _idToObject.Clear();
            foreach (var scriptableObject in AllData)
            {
                var uniqueObject = (IUniqueObject)scriptableObject;
                if (string.IsNullOrWhiteSpace(uniqueObject.Identifier))
                {
                    continue;
                }

                _idToObject.TryAdd(uniqueObject.Identifier, uniqueObject);
            }
        }

        #endregion
    }
}