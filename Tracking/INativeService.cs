using System;

namespace Tracking
{
    public interface INativeService
    {
        void StartTracking(Action<string> locationChangedHandler);

        void StopTracking();
    }
}
