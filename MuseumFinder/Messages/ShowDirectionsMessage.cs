using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace MuseumFinder.Messages 
{
    public class ShowDirectionsMessage : MessageBase
    {
        public string Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
    }
}
