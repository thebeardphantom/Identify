using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueObjectDataStore
    {
        #region Methods

        IUniqueObject FindUniqueObject(PropertyName identifier);

        #endregion
    }
}