using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace MuseumFinder.Messages
{
    public class CallPhoneNumberMessage : MessageBase
    {
        public string PhoneNumber { get; set; }
    }
}
