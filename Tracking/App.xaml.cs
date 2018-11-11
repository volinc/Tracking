using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tracking
{
    public partial class App : Application
    {
        public App(IAppService appService)
        {
            InitializeComponent();

            var mainPage = new MainPage
            {
                BindingContext = new MainViewModel(appService)
            };

            MainPage = mainPage;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
