using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MuseumFinder.Domain;

namespace MuseumFinder.Messages
{
    public class ShowDetailsMessage : MessageBase
    {
        public Address Address { get; set; }
    }
}
