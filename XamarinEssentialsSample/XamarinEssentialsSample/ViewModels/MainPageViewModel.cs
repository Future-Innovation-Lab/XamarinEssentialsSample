using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinEssentialsSample.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand GetGpsPositionCommand { get; set; }
        public ICommand OpenMapCommandAtPositionCommand { get; set; }

        private Location _currentLocation;

        public Location CurrentLocation
        {
            get
            {
                return _currentLocation;
            }
            set
            {
                _currentLocation = value;

                OnPropertyChanged();
            }
        }

        private MapSpan _centerPosition;
        public MapSpan CenterPosition
        {
            get { return _centerPosition; }
            set { _centerPosition = value;
                OnPropertyChanged(); }
        }

        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            GetGpsPositionCommand = new Command(GetGpsPosition);
            OpenMapCommandAtPositionCommand = new Command(OpenMapCommandAtPosition);
        }

        private async void OpenMapCommandAtPosition(object obj)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            if (location != null)
            {
                CenterPosition = MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(10));

            }

           
        }

        private async void GetGpsPosition(object obj)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            if (location != null)
            {
                CurrentLocation = location;

            }
        }
    }
}

