using Android.OS;

namespace Tracking.Droid.Services
{
    public class HybridServiceBinder : Binder  
    {
        public HybridServiceBinder(HybridService hybridService)  
        {  
            HybridService = hybridService;  
        }

        public HybridService HybridService { get; }

        public bool IsBound { get; set; }
    }
}