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
            Task.Run(() =>
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
                
                var locationServiceIntent = new Intent(Application.Context, typeof(MyService));
                Console.WriteLine("Calling service binding");
                
                Application.Context.BindService(locationServiceIntent, MyServiceConnection, Bind.AutoCreate);
            });
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