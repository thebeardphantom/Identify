using System;
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
        public override void RebuildDataStore(bool force = false)
        {
            if (!force && AllData.Count == IdToObject.Count)
            {
                return;
            }

            IdToObject.Clear();
            foreach (var data in AllData)
            {
                IUniqueObject uniqueObject;
                try
                {
                    uniqueObject = (IUniqueObject)data;
                }
                catch (Exception)
                {
                    Debug.LogError($"Cannot cast {data} to IUniqueObject.");
                    throw;
                }

                if (string.IsNullOrWhiteSpace(uniqueObject.Identifier))
                {
                    Debug.LogError($"Asset {data} has not generated its Identifier.", data);
                }

                if (!IdToObject.TryAdd(uniqueObject.Identifier, uniqueObject))
                {
                    var existing = (ScriptableObject)IdToObject[uniqueObject.Identifier];
                    Debug.LogError($"Asset {data} has duplicate Identifier with existing asset {existing}.", data);
                }
            }
        }

        #endregion
    }
}