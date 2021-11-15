using BeardPhantom.Identify;
using UnityEngine;

[CreateAssetMenu]
public class TestAsset : UniqueScriptableObject
{
    #region Properties

    [field: SerializeField]
    public int Data { get; private set; }

    #endregion
}