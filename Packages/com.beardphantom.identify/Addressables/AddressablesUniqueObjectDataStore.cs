#if ADDRESSABLES_EXTENSIONS_ENABLED
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BeardPhantom.Identify.Addressables
{
    [CreateAssetMenu(menuName = "CUSTOM/" + nameof(AddressablesUniqueObjectDataStore))]
    public class AddressablesUniqueObjectDataStore : UniqueObjectDataStoreAsset
    {
        #region Fields

        private AsyncOperationHandle<IList<ScriptableObject>> _asyncOperationHandle;

        #endregion

        #region Properties

        [field: SerializeField]
        private string GroupKey { get; set; }

        #endregion

        #region Methods

        public override void RebuildDataStore(bool force = false)
        {
            IdToObject.Clear();
            if (_asyncOperationHandle.IsValid())
            {
                UnityEngine.AddressableAssets.Addressables.Release(_asyncOperationHandle);
            }

            _asyncOperationHandle =
                UnityEngine.AddressableAssets.Addressables.LoadAssetsAsync<ScriptableObject>(GroupKey, OnLoadAsset);

            _asyncOperationHandle.WaitForCompletion();
        }

        private void OnLoadAsset(ScriptableObject obj)
        {
            var uniqueObject = (IUniqueObject)obj;
            IdToObject.Add(uniqueObject.Identifier, uniqueObject);
        }

        #endregion
    }
}
#endif