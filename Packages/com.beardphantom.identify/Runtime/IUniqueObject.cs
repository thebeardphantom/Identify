using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueObject
    {
        #region Properties

        string IdentifierString { get; }

        PropertyName Identifier { get; }

        #endregion
    }
}