using GalaSoft.MvvmLight;
using MuseumFinder.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuseumFinder.Util;

namespace MuseumFinder.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IAddressRepository addressRepository;

        public SearchViewModel(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;

            SearchResults = new ObservableCollection<SearchResultViewModel>();
        }

        private string searchText;
        public string SearchText 
        {
            get { return searchText; }
            set { searchText = value; SearchAddresses(searchText); }
        }

        public ObservableCollection<SearchResultViewModel> SearchResults { get; set; }

        private void SearchAddresses(string searchText)
        {
            IEnumerable<Address> addresses = addressRepository.FindAddresses(searchText);

            SearchResults.Clear();
            addresses.ForEach(address => SearchResults.Add(
                new SearchResultViewModel() { 
                    Name = address.Name,
                    Address = (address.Street != null) ? string.Format("{0} {1}, {2} {3}", address.Street, address.HouseNumber, address.PostalCode, address.City) : address.Website,
            }));
        }
    }
}
