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
        protected static HybridServiceConnection HybridServiceConnection;

        static AppService()
        {
            Current = new AppService();
        }

        protected AppService()
        {
            HybridServiceConnection = new HybridServiceConnection(null);
            HybridServiceConnection.ServiceConnected += (sender, e) => HybridServiceConnected?.Invoke(this, e);
        }
        
        public static AppService Current { get; }

        public HybridService HybridService
        {
            get
            {
                if (HybridServiceConnection.Binder == null)
                    throw new Exception("Service not bound yet");

                return HybridServiceConnection.Binder.HybridService;
            }
        }
        
        public event EventHandler<ServiceConnectedEventArgs> HybridServiceConnected;

        public static void StartHybridService()
        {
            // Starting a service like this is blocking, so we want to do it on a background thread
            new Task(() =>
                     {
                         if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                         {
                             Console.WriteLine("Calling StartForegroundService");
                             Application.Context.StartForegroundService(new Intent(Application.Context, typeof(HybridService)));
                         }
                         else
                         {
                             Console.WriteLine("Calling StartService");
                             Application.Context.StartService(new Intent(Application.Context, typeof(HybridService)));
                         }

                         // bind our service (Android goes and finds the running service by type, and puts a reference
                         // on the binder to that service)
                         // The Intent tells the OS where to find our Service (the Context) and the Type of Service
                         // we're looking for (HybridService)
                         var locationServiceIntent = new Intent(Application.Context, typeof(HybridService));
                         Console.WriteLine("Calling service binding");

                         // Finally, we can bind to the Service using our Intent and the ServiceConnection we
                         // created in a previous step.
                         Application.Context.BindService(locationServiceIntent, HybridServiceConnection, Bind.AutoCreate);
                     }).Start();
        }

        public static void StopHybridService()
        {
            // Check for nulls in case StartLocationService task has not yet completed.
            Console.WriteLine("Calling StopHybridService");

            // Unbind from the HybridService; otherwise, StopSelf (below) will not work:
            if (HybridServiceConnection != null)
            {
                Console.WriteLine("Unbinding from HybridService");
                Application.Context.UnbindService(HybridServiceConnection);
            }

            // Stop the HybridService:
            if (Current.HybridService != null)
            {
                Console.WriteLine("Stopping the HybridService");
                Current.HybridService.StopSelf();
            }
        }
    }
}