using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MuseumFinder.Util
{
    public static class NavigationServiceExtensions
    {
        private static object navigationData = null;

        public static void Navigate(this NavigationService service, Uri source, object data)
        {
            navigationData = data;
            service.Navigate(source);
        }

        public static object GetLastNavigationData(this NavigationService service)
        {
            object data = navigationData;
            navigationData = null;
            return data;
        }
    }
}
