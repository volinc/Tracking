using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tracking.Annotations;
using Xamarin.Forms;

namespace Tracking
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IAppService appService;

        public MainViewModel(IAppService appService)
        {
            this.appService = appService;
            StartCommand = new Command(appService.Start, () => service == null);
            StopCommand = new Command(appService.Stop, () => service != null);
        }

        public void OnAppearing()
        {
            appService.NativeServiceConnected += AppServiceOnNativeServiceConnected;
        }
        
        public void OnDisappearing()
        {
            appService.NativeServiceConnected -= AppServiceOnNativeServiceConnected;
        }
        
        private void AppServiceOnNativeServiceConnected(object sender, INativeService nativeService)
        {
            service = nativeService;
            service.StartTracking(newLocation => Location = location);
        }

        private INativeService service;

        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value == location) return;

                location = value;
                OnPropertyChanged(nameof(location));
            }
        }

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
