using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MuseumFinder.Domain;
using MuseumFinder.Messages;

namespace MuseumFinder.ViewModels
{
    public class AddressViewModel
    {
        private readonly Address address;

        public AddressViewModel(Address address)
        {
            this.address = address;
            this.ShowDetails = new RelayCommand(SendDetailsMessage);
            this.GetDirections = new RelayCommand(SendDirectionsMessage);
            this.CallPhoneNumber = new RelayCommand(SendCallPhoneNumberMessage);
            this.CreateMail = new RelayCommand(SendCreateMailMessage);
            this.OpenWebPage = new RelayCommand(SendOpenWebPageMessage);
        }

        public String Name { get { return address.Name; } }
        
        public GeoCoordinate Coordinate { get { return address.Coordinate; } }

        public string AddressLine1
        {
            get
            {
                if(address.Street != null && address.HouseNumber != null)
                    return  address.Street + " " + address.HouseNumber;
                else
                    return null;
            }
        }

        public string AddressLine2
        {
            get
            {
                if (address.PostalCode != null && address.City != null)
                    return address.PostalCode + " " + address.City;
                else
                    return null;
            }
        }
        
        public string Website { get { return address.Website; } }
        
        public string PhoneNumber { get { return address.PhoneNumber; } }
        
        public string EmailAddress { get { return address.EmailAddress; } }

        public ICommand ShowDetails { get; private set; }

        public ICommand GetDirections { get; private set; }

        public ICommand CallPhoneNumber { get; private set; }

        public ICommand CreateMail { get; private set; }

        public ICommand OpenWebPage { get; private set; }

        private void SendDetailsMessage()
        {
            Messenger.Default.Send(new ShowDetailsMessage() { Address = address });
        }

        private void SendDirectionsMessage()
        {
            Messenger.Default.Send(new ShowDirectionsMessage() { Coordinate = address.Coordinate });
        }

        private void SendCallPhoneNumberMessage()
        {
            Messenger.Default.Send(new CallPhoneNumberMessage() { PhoneNumber = address.PhoneNumber });
        }

        private void SendCreateMailMessage()
        {
            Messenger.Default.Send(new CreateMailMessage() { EmailAddress = address.EmailAddress });
        }

        private void SendOpenWebPageMessage()
        {
            Messenger.Default.Send(new OpenWebPageMessage() { Url = address.Website });
        }
    }
}
