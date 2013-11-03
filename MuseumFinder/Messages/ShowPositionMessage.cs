using System.Device.Location;
using GalaSoft.MvvmLight.Messaging;

namespace MuseumFinder.Messages
{
    public class ShowPositionMessage : MessageBase
    {
        public GeoCoordinate Coordinate { get; set; }
    }
}
