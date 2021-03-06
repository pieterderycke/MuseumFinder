﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MuseumFinder.ViewModels;
using MuseumFinder.Util;

namespace MuseumFinder.Screens
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            this.DataContext = new SettingsViewModel();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendCurrentView();
        }
    }
}