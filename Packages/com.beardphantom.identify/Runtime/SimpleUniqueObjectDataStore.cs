using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(SimpleUniqueObjectDataStore))]
    public partial class SimpleUniqueObjectDataStore : ScriptableObject, IUniqueObjectDataStore
    {
        #region Fields

        private readonly Dictionary<string, UniqueScriptableObject> _idToObject = new();

        private readonly OncePerRuntimeSessionToken _onceToken = new();

        #endregion

        #region Properties

        public IEnumerable<UniqueScriptableObject> Data => AllData;

        [field: SerializeField]
        private List<UniqueScriptableObject> AllData { get; set; } = new();

        #endregion

        #region Methods

        public void BuildDatastore()
        {
            if (!_onceToken.TryPerformOperation() && AllData.Count == _idToObject.Count)
            {
                return;
            }

            _idToObject.Clear();
            foreach (var uniqueScriptableObject in AllData)
            {
                _idToObject.Add(uniqueScriptableObject.Identifier, uniqueScriptableObject);
            }
        }

        /// <inheritdoc />
        public bool TryFindUniqueObject(string identifier, out IUniqueObject result)
        {
            var didFind = _idToObject.TryGetValue(identifier, out var soResult);
            result = soResult;
            return didFind;
        }

        private void OnEnable()
        {
            BuildDatastore();
        }

        #endregion
    }
}