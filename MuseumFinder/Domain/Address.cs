using System;
using System.Device.Location;

namespace MuseumFinder.Domain
{
    public class Address
    {
        public String Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
