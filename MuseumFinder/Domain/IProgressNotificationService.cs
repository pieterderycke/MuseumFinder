using System;
namespace MuseumFinder.Domain
{
    public interface IProgressNotificationService
    {
        void ShowProgressMessage(string message);
        void StopProgressNotification();
    }
}
