using System;
using Android.OS;

namespace Tracking.Droid.Services
{
    public class ServiceConnectedEventArgs : EventArgs
    {
        public ServiceConnectedEventArgs(IBinder binder)
        {
            Binder = binder;
        }

        public IBinder Binder { get; }
    }
}