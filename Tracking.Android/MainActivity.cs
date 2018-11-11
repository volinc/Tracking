using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Tracking.Droid
{
    [Activity(Label = "Tracking", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            appService = new AppService();
            LoadApplication(new App(appService));
        }

        private AppService appService;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            appService.Start();
            base.OnCreate(savedInstanceState, persistentState);
        }
        
        protected override void OnDestroy()
        {
            appService.Stop();
            base.OnDestroy();
        }
    }
}