using Xamarin.Forms;

namespace Tracking
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            (BindingContext as MainViewModel)?.OnAppearing();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            (BindingContext as MainViewModel)?.OnDisappearing();
            base.OnDisappearing();
        }
    }
}
