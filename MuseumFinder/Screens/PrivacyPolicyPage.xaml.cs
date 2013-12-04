using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MuseumFinder.Util;

namespace MuseumFinder.Screens
{
    public partial class PrivacyPolicyPage : PhoneApplicationPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendCurrentView();
        }
    }
}