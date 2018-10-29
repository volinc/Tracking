using Android.App;
using Android.Content;
using Android.OS;
using Console = System.Console;

namespace Tracking.Droid.Services
{
    [Service(Exported = false, Enabled = true, Name = "com.myaki.TrackingService")]
    public class HybridService : Service
    {
        public override void OnCreate()
        {
            Console.WriteLine($"{nameof(HybridService)}.{nameof(OnCreate)}");
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Console.WriteLine($"{nameof(HybridService)}.{nameof(OnStartCommand)}");

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            Console.WriteLine($"{nameof(HybridService)}.{nameof(OnBind)}");

            return new HybridServiceBinder(this);
        }

        public override void OnDestroy()
        {
            Console.WriteLine($"{nameof(HybridService)}.{nameof(OnDestroy)}");

            base.OnDestroy();
        }
    }
}