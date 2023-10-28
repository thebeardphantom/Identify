using UnityEditor;

namespace BeardPhantom.Identify
{
    [InitializeOnLoad]
    internal static partial class RuntimeSessionHelper
    {
        #region Constructors

        static RuntimeSessionHelper()
        {
            EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
        }

        #endregion

        #region Methods

        private static void OnPlaymodeStateChanged(PlayModeStateChange obj)
        {
            _isPlaying = obj is PlayModeStateChange.EnteredPlayMode;
        }

        #endregion
    }
}