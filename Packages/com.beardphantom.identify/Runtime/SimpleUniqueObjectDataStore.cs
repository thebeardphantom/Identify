using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(SimpleUniqueObjectDataStore))]
    public partial class SimpleUniqueObjectDataStore : ScriptableObject, IUniqueObjectDataStore
    {
        #region Fields

        private readonly Dictionary<PropertyName, IUniqueObject> _idToObject = new();

        private readonly OncePerRuntimeSessionToken _onceToken = new();

        #endregion

        #region Properties

        public IEnumerable<IUniqueObject> Data => _idToObject.Values;

        [field: SerializeField]
        private bool BuildLazily { get; set; }

        [field: SerializeField]
        private List<ScriptableObject> AllData { get; set; } = new();

        #endregion

        #region Methods

        public void RebuildDatastore(bool force = false)
        {
            if (!force
                && AllData.Count == _idToObject.Count
                && _onceToken.TryPerformOperation() != OncePerRuntimeSessionToken.State.JustTriggered)
            {
                return;
            }

            _idToObject.Clear();
            foreach (var data in AllData)
            {
                var uniqueObject = (IUniqueObject)data;
                _idToObject.Add(uniqueObject.IdentifierFast, uniqueObject);
            }
        }

        /// <inheritdoc />
        public bool TryFindUniqueObject(PropertyName identifier, out IUniqueObject result)
        {
            if (BuildLazily)
            {
                RebuildDatastore();
            }

            var didFind = _idToObject.TryGetValue(identifier, out var soResult);
            result = soResult;
            return didFind;
        }

        private void OnEnable()
        {
            RebuildDatastore(true);
        }

        #endregion
    }
}