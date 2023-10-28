using System;

namespace BeardPhantom.Identify
{
    internal class OncePerRuntimeSessionToken
    {
        #region Types

        public enum State
        {
            NotInRuntime = 0,
            AlreadyTriggered = 1,
            JustTriggered = 2
        }

        #endregion

        #region Fields

        private Guid? _lastExecSessionGuid;

        #endregion

        #region Methods

        public State TryPerformOperation()
        {
            if (!RuntimeSessionHelper.IsPlaying)
            {
                return State.NotInRuntime;
            }

            if (_lastExecSessionGuid.HasValue && RuntimeSessionHelper.SessionGuid == _lastExecSessionGuid)
            {
                return State.AlreadyTriggered;
            }

            _lastExecSessionGuid = RuntimeSessionHelper.SessionGuid;
            return State.JustTriggered;

        }

        #endregion
    }
}