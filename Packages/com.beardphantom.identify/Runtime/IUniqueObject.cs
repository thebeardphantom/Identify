using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueObject
    {
        #region Properties

        string Identifier { get; }

        PropertyName IdentifierFast { get; }

        #endregion
    }
}