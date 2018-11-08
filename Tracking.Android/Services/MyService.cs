using Android.App;
using Android.Content;
using Android.OS;
using Console = System.Console;

namespace Tracking.Droid.Services
{
    [Service(Exported = false, Enabled = true, Name = "com.myaki.TrackingService")]
    public class MyService : Service
    {
        public override void OnCreate()
        {
            Console.WriteLine("MyService.OnCreate");
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Console.WriteLine("MyService.OnStartCommand");

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            Console.WriteLine("MyService.OnBind");

            return new MyServiceBinder(this);
        }

        public override void OnDestroy()
        {
            Console.WriteLine("MyService.OnDestroy");

            base.OnDestroy();
        }
    }
}