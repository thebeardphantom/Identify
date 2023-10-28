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
                if (string.IsNullOrWhiteSpace(uniqueObject.Identifier))
                {
                    Debug.LogError($"Asset {data} has not generated its Identifier.", data);
                }

                if (!_idToObject.TryAdd(uniqueObject.Identifier, uniqueObject))
                {
                    var existing = (ScriptableObject)_idToObject[uniqueObject.Identifier];
                    Debug.LogError($"Asset {data} has duplicate Identifier with existing asset {existing}.", data);
                }
            }
        }

        /// <inheritdoc />
        public bool TryFindUniqueObject(string identifier, out IUniqueObject result)
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