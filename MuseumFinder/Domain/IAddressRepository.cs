using System.Collections.Generic;
using System.Device.Location;

namespace MuseumFinder.Domain
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAddresses();
        Address GetNearestAddress(GeoCoordinate coordinate);
    }
}