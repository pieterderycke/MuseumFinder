using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumFinder.Domain
{
    public class ProgressNotificationService : MuseumFinder.Domain.IProgressNotificationService
    {
        private ProgressIndicator progress;

        public void ShowProgressMessage(string message)
        {
            if(progress == null)
            {
                progress = new ProgressIndicator();
                progress.IsVisible = true;
                progress.IsIndeterminate = true;
                progress.Text = message;
            }

            SystemTray.ProgressIndicator = progress;
        }

        public void StopProgressNotification()
        {
            progress.IsVisible = false;
            progress = null;
            SystemTray.ProgressIndicator = null;
        }
    }
}
