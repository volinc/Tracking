using Android.OS;

namespace Tracking.Droid.Services
{
    public class MyServiceBinder : Binder  
    {
        public MyServiceBinder(MyService myService)  
        {  
            MyService = myService;  
        }

        public MyService MyService { get; }

        public bool IsBound { get; set; }
    }
}