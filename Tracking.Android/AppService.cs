using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Tracking.Droid.Services;

namespace Tracking.Droid
{
    public class AppService : IAppService
    {
        private readonly MyServiceConnection myServiceConnection;
        private MyService myService;

        public AppService()
        {
            myServiceConnection = new MyServiceConnection();
            myServiceConnection.ServiceConnected += (sender, e) =>
            {
                myService = ((MyServiceBinder)e.Binder).MyService;
                NativeServiceConnected?.Invoke(this, myService);
            };
        }

        public event EventHandler<INativeService> NativeServiceConnected;
        
        public void Start()
        {
            Task.Run(() =>
            {
                var serviceIntent = new Intent(Application.Context, typeof(MyService));
                
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    Application.Context.StartForegroundService(serviceIntent);
                else
                    Application.Context.StartService(serviceIntent);
                
                Application.Context.BindService(serviceIntent, myServiceConnection, Bind.AutoCreate);
            });
        }

        public void Stop()
        {
            if (myServiceConnection != null)
            {
                Application.Context.UnbindService(myServiceConnection);

                if (myService != null)
                {
                    myService.StopTracking();
                    myService.StopSelf();
                }
            }
        }
    }
}