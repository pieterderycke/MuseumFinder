using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MuseumFinder.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IsolatedStorageSettings settings;

        public SettingsViewModel()
        {
            this.settings = IsolatedStorageSettings.ApplicationSettings;            
        }

        public bool UseLocationService
        {
            get { 
                if(settings.Contains(App.UseLocationServiceKey))
                    return (bool)settings[App.UseLocationServiceKey];
                else
                    return true;
            } 
            set { settings[App.UseLocationServiceKey] = value; settings.Save(); }
        }
    }
}
