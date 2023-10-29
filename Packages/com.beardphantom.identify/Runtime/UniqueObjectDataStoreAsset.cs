using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    public abstract class UniqueObjectDataStoreAsset : ScriptableObject, IUniqueObjectDataStore
    {
        #region Fields

        protected readonly Dictionary<string, IUniqueObject> IdToObject = new();

        protected readonly OncePerRuntimeSessionToken LoadToken = new();

        #endregion

        #region Properties

        public IEnumerable<IUniqueObject> Data => IdToObject.Values;

        [field: SerializeField]
        private bool BuildOnEnable { get; set; }

        [field: SerializeField]
        private bool BuildLazily { get; set; }

        #endregion

        #region Methods

        public abstract void RebuildDataStore(bool force = false);

        /// <inheritdoc />
        public bool TryFindUniqueObject(string identifier, out IUniqueObject result)
        {
            if (BuildLazily)
            {
                RebuildDataStoreIfNecessary();
            }

            return IdToObject.TryGetValue(identifier, out result);
        }

        private void RebuildDataStoreIfNecessary(bool force = false)
        {
            if (force || LoadToken.TryPerformOperation() == OncePerRuntimeSessionToken.State.JustTriggered)
            {
                RebuildDataStore(force);
            }
        }

        private void OnEnable()
        {
            if (BuildOnEnable)
            {
                RebuildDataStoreIfNecessary(true);
            }
        }

        #endregion
    }
}