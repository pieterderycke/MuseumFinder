using System.Collections.Generic;
using System.Device.Location;

namespace MuseumFinder.Domain
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAddresses();

        IEnumerable<Address> FindAddresses(string searchText);

        Address GetNearestAddress(GeoCoordinate coordinate);
    }
}