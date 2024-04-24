using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BeardPhantom.Identify
{
    public abstract class UniqueObjectDataStoreAsset : ScriptableObject, IUniqueObjectDataStore
    {
        #region Fields

        protected readonly Dictionary<string, IUniqueObject> IdToObject = new();

        protected readonly Dictionary<Type, HashSet<IUniqueObject>> ObjectsByType = new();

        #endregion

        #region Properties

        public IEnumerable<IUniqueObject> Data => IdToObject.Values;

        #endregion

        #region Methods

        public abstract void RebuildDataStore();

        public abstract Awaitable RebuildDataStoreAsync();

        /// <inheritdoc />
        public bool TryFindUniqueObject(string identifier, out IUniqueObject result)
        {
            return IdToObject.TryGetValue(identifier, out result);
        }

        /// <inheritdoc />
        public IEnumerable<T> GetAllOfType<T>()
        {
            var set = ObjectsByType[typeof(T)];
            return set.Cast<T>();
        }

        protected void RegisterData(IUniqueObject data)
        {
            if (string.IsNullOrWhiteSpace(data.Identifier))
            {
                Debug.LogError($"UniqueObject {data} has not generated its Identifier.", data as Object);
            }

            if (!IdToObject.TryAdd(data.Identifier, data))
            {
                var existing = IdToObject[data.Identifier];
                Debug.LogError($"Asset {data} has duplicate Identifier with existing asset {existing}.", data as Object);
            }

            var type = data.GetType();
            if (!ObjectsByType.TryGetValue(type, out var set))
            {
                set = new HashSet<IUniqueObject>();
                ObjectsByType[type] = set;
            }

            set.Add(data);
        }

        #endregion
    }
}