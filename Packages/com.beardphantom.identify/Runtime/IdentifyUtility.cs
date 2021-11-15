#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class IdentifyUtility
{
    #region Methods

    public static Hash128 GetObjectIdAsHash128(Object obj)
    {
        var id = GlobalObjectId.GetGlobalObjectIdSlow(obj);
        var guidHash = Hash128.Compute(id.ToString());
        return guidHash;
    }

    #endregion
}
#endif