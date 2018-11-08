using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Tracking.Droid.Services;

namespace Tracking.Droid
{
    public class AppService
    {
        protected static MyServiceConnection MyServiceConnection;

        static AppService()
        {
            Current = new AppService();
        }

        protected AppService()
        {
            MyServiceConnection = new MyServiceConnection();
            MyServiceConnection.ServiceConnected += (sender, e) => HybridServiceConnected?.Invoke(this, e);
        }
        
        public static AppService Current { get; }

        public MyService MyService
        {
            get
            {
                if (MyServiceConnection.Binder == null)
                    throw new Exception("Service not bound yet");

                return MyServiceConnection.Binder.MyService;
            }
        }
        
        public event EventHandler<ServiceConnectedEventArgs> HybridServiceConnected;

        public static void StartMyService()
        {
            // Starting a service like this is blocking, so we want to do it on a background thread
            new Task(() =>
                     {
                         if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                         {
                             Console.WriteLine("Calling StartForegroundService");
                             Application.Context.StartForegroundService(new Intent(Application.Context, typeof(MyService)));
                         }
                         else
                         {
                             Console.WriteLine("Calling StartService");
                             Application.Context.StartService(new Intent(Application.Context, typeof(MyService)));
                         }

                         // bind our service (Android goes and finds the running service by type, and puts a reference
                         // on the binder to that service)
                         // The Intent tells the OS where to find our Service (the Context) and the Type of Service
                         // we're looking for (MyService)
                         var locationServiceIntent = new Intent(Application.Context, typeof(MyService));
                         Console.WriteLine("Calling service binding");

                         // Finally, we can bind to the Service using our Intent and the ServiceConnection we
                         // created in a previous step.
                         Application.Context.BindService(locationServiceIntent, MyServiceConnection, Bind.AutoCreate);
                     }).Start();
        }

        public static void StopMyService()
        {
            if (MyServiceConnection != null)
            {
                Console.WriteLine("Unbinding from MyService");
                Application.Context.UnbindService(MyServiceConnection);
            }

            if (Current.MyService != null)
            {
                Console.WriteLine("Stopping the MyService");
                Current.MyService.StopSelf();
            }
        }
    }
}