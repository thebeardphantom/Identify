using System;

namespace BeardPhantom.Identify
{
    internal static partial class RuntimeSessionHelper
    {
        #region Fields

        private static Guid? _sessionGuid;

        private static bool _isPlaying;

        #endregion

        #region Properties

        public static Guid SessionGuid
        {
            get
            {
                if (!IsPlaying)
                {
                    _sessionGuid = default;
                    return default;
                }

                _sessionGuid ??= Guid.NewGuid();
                return _sessionGuid.Value;
            }
        }

        internal static bool IsPlaying
        {
            get
            {
#if UNITY_EDITOR
                return _isPlaying;
#else
                return true;
#endif
            }
        }

        #endregion
    }
}