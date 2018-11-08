using System;
using Android.Content;
using Android.OS;

namespace Tracking.Droid.Services
{
    public class MyServiceConnection : Java.Lang.Object, IServiceConnection 
    {
        public MyServiceBinder Binder { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder binder)
        {
            Console.WriteLine($"MyServiceConnection.OnServiceConnected : {binder.GetType().Name}");

            if (binder is MyServiceBinder serviceBinder)
            {
                Binder = serviceBinder;
                Binder.IsBound = true;

                ServiceConnected?.Invoke(this, new ServiceConnectedEventArgs(binder));
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Console.WriteLine($"{nameof(MyServiceConnection)}.{nameof(OnServiceDisconnected)}");

            Binder.IsBound = false;
        }

        public event EventHandler<ServiceConnectedEventArgs> ServiceConnected;
    }
}