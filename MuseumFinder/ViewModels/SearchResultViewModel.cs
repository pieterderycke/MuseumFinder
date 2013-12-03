using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MuseumFinder.Domain;
using MuseumFinder.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MuseumFinder.ViewModels
{
    public class SearchResultViewModel : ViewModelBase
    {
        private Address address;

        public SearchResultViewModel(Address address)
	    {
            this.address = address;
            this.ShowDetails = new RelayCommand(SendDetailsMessage);
	    }

        public string Name { get { return address.Name; } }

        public string Address { get { return (address.Street != null) ? string.Format("{0} {1}, {2} {3}", address.Street, address.HouseNumber, address.PostalCode, address.City) : address.Website; } }

        public ICommand ShowDetails { get; private set; }

        private void SendDetailsMessage()
        {
            Messenger.Default.Send(new ShowDetailsMessage() { Address = address });
        }
    }
}
