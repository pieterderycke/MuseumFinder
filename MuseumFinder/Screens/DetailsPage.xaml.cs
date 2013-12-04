using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MuseumFinder.Domain;
using MuseumFinder.ViewModels;
using Nokia.Phone.HereLaunchers;
using MuseumFinder.Messages;
using MuseumFinder.Util;

namespace MuseumFinder.Screens
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();

            Address address = (Address) NavigationService.GetLastNavigationData();
            DataContext = new AddressViewModel(address);
        }
    }
}