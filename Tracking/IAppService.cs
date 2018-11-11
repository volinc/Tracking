using System;

namespace Tracking
{
    public interface IAppService
    {
        event EventHandler<INativeService> NativeServiceConnected;

        void Start();

        void Stop();
    }
}
