using System;
using Android.App;
using Android.Content;
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
            LoadApplication(new App());

            Console.WriteLine(AppService.Current);
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            AppService.StartHybridService();

            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnNewIntent(Intent intent)
        {
            
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            AppService.StopHybridService();
        }
    }
}