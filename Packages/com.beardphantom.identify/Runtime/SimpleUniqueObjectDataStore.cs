using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.Identify
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(SimpleUniqueObjectDataStore))]
    public partial class SimpleUniqueObjectDataStore : UniqueObjectDataStoreAsset
    {
        #region Properties

        [field: SerializeField]
        private List<ScriptableObject> AllData { get; set; } = new();

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void RebuildDataStore()
        {
            IdToObject.Clear();
            ObjectsByType.Clear();
            foreach (var data in AllData)
            {
                RegisterData((IUniqueObject)data);
            }
        }

        /// <inheritdoc />
        public override Awaitable RebuildDataStoreAsync()
        {
            RebuildDataStore();
            return default;
        }

        #endregion
    }
}