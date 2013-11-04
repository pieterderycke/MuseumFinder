using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Shell;
using MuseumFinder.Messages;
using MuseumFinder.ViewModels;
using MuseumFinder.Resources;
using Nokia.Phone.HereLaunchers;
using Windows.Devices.Geolocation;
using MuseumFinder.Util;

namespace MuseumFinder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            Messenger.Default.Register<ShowPositionMessage>(this, OnShowPositionMessageReceived);
            Messenger.Default.Register<ShowDetailsMessage>(this, OnShowDetailsMessageReceived);

            MainViewModel viewModel = new MainViewModel();
            this.DataContext = viewModel;

            // MVVM was to slow with a lot of pushpins :(
            MapLayer layer = new MapLayer();
            foreach (AddressViewModel address in viewModel.Addresses)
            {
                MapOverlay overlay = new MapOverlay();
                Pushpin pushpin = new Pushpin() {Content = address.Name};
                pushpin.Tap += (sender, e) => { address.ShowDetails.Execute(null); };
                overlay.Content = pushpin;
                overlay.GeoCoordinate = address.Coordinate;

                layer.Add(overlay);
            }
            addressMap.Layers.Add(layer);

            //MapItemsControl mapItems = MapExtensions.GetChildren(addressMap).FirstOrDefault(c => c is MapItemsControl) as MapItemsControl;
            //mapItems.ItemsSource = viewModel.Addresses;

            addressMap.Loaded += addressMap_Loaded;

            CenterBrussels();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ((MainViewModel)DataContext).RefreshSettings();
        }

        private void CenterBrussels()
        {
            // Make my current location the center of the Map.
            this.addressMap.Center = new GeoCoordinate(51.053468, 3.73038);
            this.addressMap.ZoomLevel = 10;
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton locateMeButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.locateme.png", UriKind.Relative));
            locateMeButton.Text = "Locate Me";
            locateMeButton.Click += locateMeButton_Click;
            ApplicationBar.Buttons.Add(locateMeButton);

            ApplicationBarIconButton nearestButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.nearby.png", UriKind.Relative));
            nearestButton.Text = "Nearest";
            nearestButton.Click += nearestButton_Click;
            ApplicationBar.Buttons.Add(nearestButton);

            ApplicationBarIconButton directionsButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.directions.png", UriKind.Relative));
            directionsButton.Text = "GetDirections";
            directionsButton.Click += directionsButton_Click;
            ApplicationBar.Buttons.Add(directionsButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem privacyPolicyMenuItem = new ApplicationBarMenuItem("Privacy Policy");
            privacyPolicyMenuItem.Click += privacyPolicyMenuItem_Click;
            ApplicationBar.MenuItems.Add(privacyPolicyMenuItem);

            ApplicationBarMenuItem aboutMenuItem = new ApplicationBarMenuItem("About");
            aboutMenuItem.Click += aboutMenuItem_Click;
            ApplicationBar.MenuItems.Add(aboutMenuItem);
        }

        private void addressMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "323e03d4-fb8c-4d43-81f4-f73ab51a7318";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "FffTXwxA7ZBKl2P7GGs_yw";
        }

        private void locateMeButton_Click(object sender, EventArgs e)
        {
            ((MainViewModel)this.DataContext).LocateMe.Execute(null);
        }

        private void nearestButton_Click(object sender, EventArgs e)
        {
            ((MainViewModel)this.DataContext).Nearest.Execute(null);
        }

        private void directionsButton_Click(object sender, EventArgs e)
        {
            ((MainViewModel)this.DataContext).Directions.Execute(null);
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox messageBox = new CustomMessageBox()
            {
                Caption = "About this app",
                Message = string.Format("\"Museum Finder\" is an app for WP8 that helps you finding nearby museums in Flanders.\n\nThis app was made possible thanks to the open data sets of the Flemish government."),
                LeftButtonContent = "close",
            };
            messageBox.Show();
            ;
        }

        private void privacyPolicyMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrivacyPolicyPage.xaml", UriKind.Relative));
        }

        private void OnShowPositionMessageReceived(ShowPositionMessage message)
        {
            // Make my current location the center of the Map.
            this.addressMap.Center = message.Coordinate;
            this.addressMap.ZoomLevel = 17;
        }

        private void OnShowDetailsMessageReceived(ShowDetailsMessage message)
        {
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative), message.Address);
        }
    }
}