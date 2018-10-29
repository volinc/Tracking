using System;
using Android.Content;
using Android.OS;

namespace Tracking.Droid.Services
{
    public class HybridServiceConnection : Java.Lang.Object, IServiceConnection 
    {
        public HybridServiceConnection(HybridServiceBinder binder)
        {
            if (Binder != null)
                Binder = binder;
        }

        public HybridServiceBinder Binder
        {
            get;
            private set;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Console.WriteLine($"{nameof(HybridServiceConnection)}.{nameof(OnServiceConnected)}");

            if (service is HybridServiceBinder serviceBinder)
            {
                Binder = serviceBinder;
                Binder.IsBound = true;

                ServiceConnected?.Invoke(this, new ServiceConnectedEventArgs
                {
                    Binder = service
                });

                //serviceBinder.HybridService.StartLocationUpdates();
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Console.WriteLine($"{nameof(HybridServiceConnection)}.{nameof(OnServiceDisconnected)}");

            Binder.IsBound = false;
        }

        public event EventHandler<ServiceConnectedEventArgs> ServiceConnected;
    }
}