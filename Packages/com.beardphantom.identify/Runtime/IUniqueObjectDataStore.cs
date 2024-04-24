using System.Collections.Generic;

namespace BeardPhantom.Identify
{
    public interface IUniqueObjectDataStore
    {
        #region Methods

        bool TryFindUniqueObject(string identifier, out IUniqueObject result);

        IEnumerable<T> GetAllOfType<T>();

        #endregion
    }
}