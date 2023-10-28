using System;

namespace BeardPhantom.Identify
{
    internal class OncePerRuntimeSessionToken
    {
        #region Fields

        private Guid? _lastExecSessionGuid;

        #endregion

        #region Methods

        public bool TryPerformOperation()
        {
            if (!RuntimeSessionHelper.IsPlaying)
            {
                return false;
            }

            if (!_lastExecSessionGuid.HasValue || RuntimeSessionHelper.SessionGuid != _lastExecSessionGuid)
            {
                _lastExecSessionGuid = RuntimeSessionHelper.SessionGuid;
                return true;
            }

            return false;
        }

        #endregion
    }
}