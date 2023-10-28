using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueObjectDataStore
    {
        #region Methods

        bool TryFindUniqueObject(string identifier, out IUniqueObject result);

        #endregion
    }
}