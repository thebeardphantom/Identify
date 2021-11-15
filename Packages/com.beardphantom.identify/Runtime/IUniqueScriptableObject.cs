using UnityEngine;

namespace BeardPhantom.Identify
{
    public interface IUniqueScriptableObject
    {
        #region Properties

        Hash128 GuidHash { get; }

        #endregion
    }
}