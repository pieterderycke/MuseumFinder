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
using MuseumFinder.Domain;

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

            MainViewModel viewModel = new MainViewModel(new AddressRepository(), new ProgressNotificationService());
            this.DataContext = viewModel;
            viewModel.PropertyChanged += viewModel_PropertyChanged; // Hook in for ApplicationBar changes

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
            locateMeButton.Text = AppResources.LocateMeIconButtonText;
            locateMeButton.Click += locateMeButton_Click;
            ApplicationBar.Buttons.Add(locateMeButton);

            ApplicationBarIconButton nearestButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.nearby.png", UriKind.Relative));
            nearestButton.Text = AppResources.NearestIconButtonText;
            nearestButton.Click += nearestButton_Click;
            ApplicationBar.Buttons.Add(nearestButton);

            ApplicationBarIconButton directionsButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.directions.png", UriKind.Relative));
            directionsButton.Text = AppResources.GetDirectionsIconButtonText;
            directionsButton.Click += directionsButton_Click;
            ApplicationBar.Buttons.Add(directionsButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem(AppResources.SettingsMenuItemText);
            settingsMenuItem.Click += settingsMenuItem_Click;
            ApplicationBar.MenuItems.Add(settingsMenuItem);

            ApplicationBarMenuItem privacyPolicyMenuItem = new ApplicationBarMenuItem(AppResources.PrivacyPolicyMenuItemText);
            privacyPolicyMenuItem.Click += privacyPolicyMenuItem_Click;
            ApplicationBar.MenuItems.Add(privacyPolicyMenuItem);

            ApplicationBarMenuItem aboutMenuItem = new ApplicationBarMenuItem(AppResources.AboutMenuItemText);
            aboutMenuItem.Click += aboutMenuItem_Click;
            ApplicationBar.MenuItems.Add(aboutMenuItem);
        }

        private void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "EnableButtons")
            {
                bool enableButtons = ((MainViewModel)DataContext).EnableButtons;
                ApplicationBar.Buttons.OfType<ApplicationBarIconButton>().ForEach(b => b.IsEnabled = enableButtons);
            }
        }

        private void addressMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = LicenseKeys.ApplicationId;
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = LicenseKeys.AuthenticationToken;
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

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void privacyPolicyMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PrivacyPolicyPage.xaml", UriKind.Relative));
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox messageBox = new CustomMessageBox()
            {
                Caption = AppResources.AboutThisApp,
                Message = string.Format(AppResources.AboutMessage),
                LeftButtonContent = AppResources.CloseButtonLabel,
            };
            messageBox.Show();
            ;
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