using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MuseumFinder.Domain;
using MuseumFinder.Messages;
using Windows.Devices.Geolocation;

namespace MuseumFinder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAddressRepository addressRepository;

        public MainViewModel()
        {
            addressRepository = new AddressRepository();
            Addresses = addressRepository.GetAddresses().Select(a => new AddressViewModel(a)).ToList();

            LocateMe = new RelayCommand(SendMyLocationMessage);
            Nearest = new RelayCommand(SendNearestLocationMessage);
            Directions = new RelayCommand(SendNearestDirectionsMessage);
        }

        private GeoCoordinate userPosition;
        public GeoCoordinate UserPosition {
            get { return userPosition; }
            private set { userPosition = value; RaisePropertyChanged("UserPosition"); }
        }

        public IEnumerable<AddressViewModel> Addresses { get; private set; }
        public ICommand LocateMe { get; private set; }
        public ICommand Nearest { get; private set; }
        public ICommand Directions { get; private set; }

        private async void SendMyLocationMessage()
        {
            GeoCoordinate coordinate = await GetCurrentPosition();

            if (coordinate != null)
            {
                Messenger.Default.Send(new ShowPositionMessage() {Coordinate = coordinate});
            }
            else
            {
                SendErrorMessage("Could not retrieve your current location, please ensure that the location service is enabled.");
            }
        }

        private async void SendNearestLocationMessage()
        {
            GeoCoordinate coordinate = await GetCurrentPosition();

            if (coordinate != null)
            {
                Address nearestAddress = addressRepository.GetNearestAddress(coordinate);

                Messenger.Default.Send(new ShowPositionMessage() {Coordinate = nearestAddress.Coordinate});
            }
            else
            {
                SendErrorMessage("Could not retrieve your current location, please ensure that the location service is enabled.");
            }
        }

        private async void SendNearestDirectionsMessage()
        {
            GeoCoordinate coordinate = await GetCurrentPosition();

            if (coordinate != null)
            {
                Address nearestAddress = addressRepository.GetNearestAddress(coordinate);

                Messenger.Default.Send(new ShowDirectionsMessage() { Name = nearestAddress.Name, Coordinate = nearestAddress.Coordinate });
            }
            else
            {
                SendErrorMessage("Could not retrieve your current location, please ensure that the location service is enabled.");
            }
        }

        private async void SendErrorMessage(string message)
        {
            Messenger.Default.Send(new ShowErrorMessage() { Message = message });
        }

        private async Task<GeoCoordinate> GetCurrentPosition()
        {
            Geolocator myGeolocator = new Geolocator();

            if (myGeolocator.LocationStatus != PositionStatus.Disabled)
            {
                Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
                GeoCoordinate coordinate = new GeoCoordinate(myGeoposition.Coordinate.Latitude,
                                                             myGeoposition.Coordinate.Longitude);
                //GeoCoordinate coordinate = new GeoCoordinate(50.853364, 4.354086);

                UserPosition = coordinate;

                return coordinate;
            }
            else
            {
                UserPosition = null;
                return null;
            }
        }
    }
}
