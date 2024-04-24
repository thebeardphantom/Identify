#if ADDRESSABLES_EXTENSIONS_ENABLED
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.AddressableAssets.Addressables;

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

        public override void RebuildDataStore()
        {
            RebuildDataStoreInternal();
            _asyncOperationHandle.WaitForCompletion();
        }

        /// <inheritdoc />
        public override async Awaitable RebuildDataStoreAsync()
        {
            RebuildDataStoreInternal();
            await _asyncOperationHandle.Task;
        }

        private void RebuildDataStoreInternal()
        {
            if (_asyncOperationHandle.IsValid() && !_asyncOperationHandle.IsDone)
            {
                _asyncOperationHandle.WaitForCompletion();
                Release(_asyncOperationHandle);
            }

            IdToObject.Clear();

            _asyncOperationHandle = LoadAssetsAsync<ScriptableObject>(GroupKey, OnLoadAsset);
            _asyncOperationHandle.Completed += OnCompleted;
        }

        private void OnCompleted(AsyncOperationHandle<IList<ScriptableObject>> asyncOperationHandle)
        {
            if (asyncOperationHandle.Equals(_asyncOperationHandle))
            {
                _asyncOperationHandle = default;
            }
        }

        private void OnLoadAsset(ScriptableObject obj)
        {
            RegisterData((IUniqueObject)obj);
        }

        #endregion
    }
}
#endif