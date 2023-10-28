using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueObjectDataStore
    {
        #region Methods

        bool TryFindUniqueObject(PropertyName identifier, out IUniqueObject result);

        #endregion
    }
}