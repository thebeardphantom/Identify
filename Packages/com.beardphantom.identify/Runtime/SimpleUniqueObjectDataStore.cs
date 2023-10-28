using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(SimpleUniqueObjectDataStore))]
    public partial class SimpleUniqueObjectDataStore : ScriptableObject, IUniqueObjectDataStore, ISerializationCallbackReceiver
    {
        #region Fields

        private readonly Dictionary<PropertyName, IUniqueObject> _nameToObject = new();

        #endregion

        #region Properties

        public IEnumerable<ScriptableObject> Data => AllData;

        [field: SerializeField]
        private List<ScriptableObject> AllData { get; set; } = new();

        #endregion

        #region Methods

        /// <inheritdoc />
        public bool TryFindUniqueObject(PropertyName identifier, out IUniqueObject result)
        {
            return _nameToObject.TryGetValue(identifier, out result);
        }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <inheritdoc />
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _nameToObject.Clear();
            foreach (var scriptableObject in AllData)
            {
                var uniqueObject = (IUniqueObject)scriptableObject;
                _nameToObject.Add(uniqueObject.Identifier, uniqueObject);
            }
        }

        #endregion
    }
}