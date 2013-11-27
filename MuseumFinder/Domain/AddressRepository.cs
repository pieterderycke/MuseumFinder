using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace MuseumFinder.Domain
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IList<Address> addresses; 

        public AddressRepository()
        {
            this.addresses = LoadAddresses();
        }

        public IEnumerable<Address> GetAddresses()
        {
            return addresses;
        }

        public Address GetNearestAddress(GeoCoordinate coordinate)
        {
            Address nearestAddress = null;
            double nearestDistance = double.MaxValue;

            foreach (Address address in addresses)
            {
                double distance = address.Coordinate.GetDistanceTo(coordinate);
                if (distance < nearestDistance)
                { 
                    nearestAddress = address;
                    nearestDistance = distance;
                }
            }

            return nearestAddress;
        }

        public IEnumerable<Address> FindAddresses(string searchText)
        {
            searchText = searchText.ToLowerInvariant();

            List<Address> result = new List<Address>();

            foreach(Address address in addresses)
            {
                if (address.Name.ToLowerInvariant().Contains(searchText))
                    result.Add(address);
                
                string fullAddressText = (address.Street + " " + address.HouseNumber + " " +
                    address.PostalCode + " " + address.City).ToLowerInvariant();

                if (fullAddressText.Contains(searchText))
                    result.Add(address);
            }

            return result;
        }

        private IList<Address> LoadAddresses()
        {
            XDocument doc = XDocument.Load("Resources/data.xml");
            return (from address in doc.Element("addresses").Elements("address")
                    let websiteElement = address.Element("website")
                    let phoneNumberElement = address.Element("phoneNumber")
                    let emailAddressElement = address.Element("emailAddress")
                    select new Address()
                    {
                        Name = address.Element("name").Value,
                        Coordinate = new GeoCoordinate(double.Parse(address.Element("latitude").Value, CultureInfo.InvariantCulture), double.Parse(address.Element("longitude").Value, CultureInfo.InvariantCulture)),
                        Website = (websiteElement != null) ? (websiteElement.Value != "") ? websiteElement.Value : null : null,
                        PhoneNumber = (phoneNumberElement != null) ? phoneNumberElement.Value : null,
                        EmailAddress = (emailAddressElement != null) ? emailAddressElement.Value : null,
                    }).ToList();
        }
    }
}
