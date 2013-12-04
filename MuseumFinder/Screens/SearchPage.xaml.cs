using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MuseumFinder.ViewModels;
using MuseumFinder.Domain;
using MuseumFinder.Util;

namespace MuseumFinder.Screens
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public SearchPage()
        {
            InitializeComponent();

            DataContext = new SearchViewModel(new AddressRepository());
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendCurrentView();
        }
    }
}